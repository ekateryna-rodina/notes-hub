using LightNote.Application.BusinessLogic.Identity.Commands;
using LightNote.Application.Contracts;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Application.Models;
using LightNote.Dal.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LightNote.Application.BusinessLogic.Identity.CommandHandlers
{
    public class LoginIdentityHandler : IRequestHandler<LoginIdentity, OperationResult<AuthenticatedResponse>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private List<Exception> _exceptions = new();
        private IdentityUser _currentIdentity = new();
        private Guid _userProfileId = default!;

        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAuthenticator _authenticator;
        public LoginIdentityHandler(
                UserManager<IdentityUser> userManager,
                IUnitOfWork unitOfWork,
                IRefreshTokenRepository refreshTokenRepository,
                IAuthenticator authenticator)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _refreshTokenRepository = refreshTokenRepository;
            _authenticator = authenticator;
        }

        public async Task<OperationResult<AuthenticatedResponse>> Handle(LoginIdentity request, CancellationToken cancellationToken)
        {
            (_currentIdentity, _userProfileId) = await ValidateUserAndReturnInfo(request);
            if (_exceptions.Any())
            {
                return OperationResult<AuthenticatedResponse>.CreateFailure(_exceptions);
            }
            // generate token
            var authenticatedResponse = await _authenticator.Authenticate(_currentIdentity.Id, _userProfileId, request.Email);
            return OperationResult<AuthenticatedResponse>.CreateSuccess(authenticatedResponse);
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

