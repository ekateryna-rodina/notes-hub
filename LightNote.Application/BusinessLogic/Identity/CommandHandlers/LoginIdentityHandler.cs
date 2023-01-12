using System;
using LightNote.Application.BusinessLogic.Identity.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Application.Services.TokenGenerators;
using LightNote.Dal.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LightNote.Application.BusinessLogic.Identity.CommandHandlers
{
    public class LoginIdentityHandler : IRequestHandler<LoginIdentity, OperationResult<(string, string)>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private List<Exception> _exceptions = new();
        private IdentityUser _currentIdentity = new();
        private Guid _userProfileId = default!;
        private readonly AccessTokenGenerator _accessTokenGenerator = default!;
        private readonly RefreshTokenGenerator _refreshTokenGenerator = default!;
        public LoginIdentityHandler(
                AccessTokenGenerator accessTokenGenerator,
                RefreshTokenGenerator refreshTokenGenerator,
                UserManager<IdentityUser> userManager,
                IUnitOfWork unitOfWork)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<(string, string)>> Handle(LoginIdentity request, CancellationToken cancellationToken)
        {
            (_currentIdentity, _userProfileId) = await ValidateUserAndReturnInfo(request);
            if (_exceptions.Any())
            {
                return OperationResult<(string, string)>.CreateFailure(_exceptions);
            }
            // generate token
            var accessToken = _accessTokenGenerator.Generate(_currentIdentity.Id, _userProfileId, request.Email);
            var refreshToken = _refreshTokenGenerator.Generate();
            return OperationResult<(string, string)>.CreateSuccess((accessToken, refreshToken));
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

