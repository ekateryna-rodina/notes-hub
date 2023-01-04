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
		public RegisterIdentityHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork, IToken tokenService)
		{
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
		}
       
        public async Task<OperationResult<string>> Handle(RegisterIdentity request, CancellationToken cancellationToken)
        {
            var existingIdentity = await _userManager.FindByEmailAsync(request.Email);
            if (existingIdentity != null) {
                var operationResult = OperationResult<string>.CreateFailure(new []{ new ResourceAlreadyExistsException("User exists")});
                return operationResult;
            }
            var newIdentity = new IdentityUser {
                Email = request.Email,
                UserName = request.Email
            };

            // Create transaction
            using var transaction = _unitOfWork.BeginTransaction();
            try
            {
                // create identity
                var result = await _userManager.CreateAsync(newIdentity, request.Password);
                if (!result.Succeeded) {
                    await transaction.RollbackAsync(cancellationToken);
                    var operationResult = OperationResult<string>.CreateFailure(result.Errors.Select(e => new Exception(e.Description)).ToArray());
                    return operationResult;
                }
                // create user profile
                var newUserId = await CreateUserProfileAsync(newIdentity, request, transaction, cancellationToken);

                // generate token
                var token = _tokenService.GenerateJwtToken(newIdentity.Id, newUserId, newIdentity.Email);
                return OperationResult<string>.CreateSuccess(token);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                var operationResult = OperationResult<string>.CreateFailure(new[] { new ResourceAlreadyExistsException(ex.Message) });
                return operationResult;
            }
        }

        private async Task<Guid> CreateUserProfileAsync(IdentityUser identity, RegisterIdentity request, IDbContextTransaction transaction, CancellationToken cancellationToken) {
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

