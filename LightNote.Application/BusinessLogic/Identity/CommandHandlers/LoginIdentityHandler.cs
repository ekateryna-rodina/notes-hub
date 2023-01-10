using System;
using LightNote.Application.BusinessLogic.Identity.Commands;
using LightNote.Application.BusinessLogic.Users.Queries;
using LightNote.Application.Contracts;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LightNote.Application.BusinessLogic.Identity.CommandHandlers
{
    public class LoginIdentityHandler : IRequestHandler<LoginIdentity, OperationResult<string>>
    {
        private readonly IToken _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private List<Exception> _exceptions = new();
        private IdentityUser _currentIdentity = new();
        private Guid _userProfileId = default!;
        public LoginIdentityHandler(IToken tokenService, UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<string>> Handle(LoginIdentity request, CancellationToken cancellationToken)
        {
            (_currentIdentity, _userProfileId) = await ValidateUserAndReturnInfo(request);
            if (_exceptions.Any())
            {
                return OperationResult<string>.CreateFailure(_exceptions);
            }
            // generate token
            var token = _tokenService.GenerateClaimsAndJwtToken(_currentIdentity.Id, _userProfileId, request.Email);
            return OperationResult<string>.CreateSuccess(_tokenService.WriteToken(token));
        }

        private async Task<(IdentityUser, Guid)> ValidateUserAndReturnInfo(LoginIdentity request)
        {
            var existingIdentity = await _userManager.FindByEmailAsync(request.Email);
            if (existingIdentity == null)
            {
                _exceptions.Add(new ResourceNotFoundException("User is not registered"));
                return (_currentIdentity, _userProfileId);
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(existingIdentity, request.Password);
            if (!isPasswordValid)
            {
                _exceptions.Add(new IncorrectPasswordException("Login is incorrect"));
                return (_currentIdentity, _userProfileId);
            }

            var userProfiles = await _unitOfWork.UserRepository.Get(u => u.IdentityId == existingIdentity.Id);
            var currentUserProfile = userProfiles.FirstOrDefault();
            if (userProfiles == null || userProfiles.FirstOrDefault() == null)
            {
                _exceptions.Add(new ResourceNotFoundException("User profile does not exist"));
                return (_currentIdentity, _userProfileId);
            }

            return (existingIdentity, currentUserProfile!.Id);
        }
    }
}

