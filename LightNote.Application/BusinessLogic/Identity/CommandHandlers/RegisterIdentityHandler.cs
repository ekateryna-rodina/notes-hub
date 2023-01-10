using System.Security.Claims;
using System.Text;
using LightNote.Application.BusinessLogic.Identity.Commands;
using LightNote.Dal;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using LightNote.Domain.Models.User;
using LightNote.Application.Options;
using Microsoft.Extensions.Options;
using LightNote.Dal.Contracts;
using LightNote.Application.Helpers;
using LightNote.Application.Exceptions;
using System.Collections.Generic;
using LightNote.Application.Contracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace LightNote.Application.BusinessLogic.Identity.CommandHandlers
{
    public class RegisterIdentityHandler : IRequestHandler<RegisterIdentity, OperationResult<string>>
    {
        private readonly IToken _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private List<Exception> _exceptions = new();
        private IdentityUser _newIdentity = new();
        private Guid _userProfileId = default!;
        public RegisterIdentityHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork, IToken tokenService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        private async Task ValidateIfUserExists(RegisterIdentity request)
        {
            var existingIdentity = await _userManager.FindByEmailAsync(request.Email);
            if (existingIdentity != null)
            {
                _exceptions.Add(new ResourceAlreadyExistsException("User exists"));
            }
        }
        private async Task<(IdentityUser, Guid)> CreateIdentityAndUserProfile(RegisterIdentity request, CancellationToken cancellationToken)
        {
            var newIdentity = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Email
            };
            // Create transaction
            using var transaction = _unitOfWork.BeginTransaction();
            try
            {
                // create identity
                var result = await _userManager.CreateAsync(newIdentity, request.Password);
                if (!result.Succeeded)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    result.Errors.ToList().ForEach(e => _exceptions.Add(new Exception(e.Description)));
                    return (newIdentity, _userProfileId);
                }

                // create user profile
                _userProfileId = await CreateUserProfileAsync(newIdentity, request, transaction, cancellationToken);
                return (newIdentity, _userProfileId);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _exceptions.Add(ex);
                return (newIdentity, _userProfileId);
            }
        }
        public async Task<OperationResult<string>> Handle(RegisterIdentity request, CancellationToken cancellationToken)
        {
            await ValidateIfUserExists(request);
            (_newIdentity, _userProfileId) = await CreateIdentityAndUserProfile(request, cancellationToken);
            if (_exceptions.Any())
            {
                return OperationResult<string>.CreateFailure(_exceptions);
            }
            // generate token
            var token = _tokenService.GenerateClaimsAndJwtToken(_newIdentity.Id, _userProfileId, _newIdentity.Email!);
            return OperationResult<string>.CreateSuccess(_tokenService.WriteToken(token));
        }

        private async Task<Guid> CreateUserProfileAsync(IdentityUser identity, RegisterIdentity request, IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            try
            {
                var basicInfo = BasicUserInfo.CreateBasicUserInfo(request.FirstName, request.LastName, request.PhotoUrl, request.Country, request.City);
                var userProfile = UserProfile.CreateUserProfile(identity.Id, basicInfo);
                _unitOfWork.UserRepository.Insert(userProfile);
                transaction.Commit();
                await _unitOfWork.SaveAsync();
                return userProfile.Id;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}

