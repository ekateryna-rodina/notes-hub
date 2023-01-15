using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class RefreshIdentityHandler : IRequestHandler<RefreshIdentity, OperationResult<AuthenticatedResponse>>
    {
        private readonly ITokenValidator _tokenValidator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticator _authenticator;
        private UserManager<IdentityUser> _userManager;
        public RefreshIdentityHandler(ITokenValidator tokenValidator,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork,
        IAuthenticator authenticator,
        UserManager<IdentityUser> userManager)
        {
            _tokenValidator = tokenValidator;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
            _authenticator = authenticator;
            _userManager = userManager;
        }
        public async Task<OperationResult<AuthenticatedResponse>> Handle(RefreshIdentity request, CancellationToken cancellationToken)
        {
            var isValid = _tokenValidator.Validate(request.RefreshToken);
            var userId = await _refreshTokenRepository.GetUserIdByRefreshTokenAsync(request.RefreshToken);
            if (!isValid || userId == Guid.Empty)
            {
                return OperationResult<AuthenticatedResponse>.CreateFailure(new[] { new InvalidTokenException() });
            }
            // get the user 
            var user = await _unitOfWork.UserRepository.GetByID(userId);
            var identity = await _userManager.FindByIdAsync(user.IdentityId);
            if (identity == null)
            {
                return OperationResult<AuthenticatedResponse>.CreateFailure(new[] { new IdentityDoesNotExistException("Identity does not exist") });
            }

            var authenticatedResponse = await _authenticator.Authenticate(user.IdentityId, user.Id.Value, identity.Email!);
            return OperationResult<AuthenticatedResponse>.CreateSuccess(authenticatedResponse);
        }
    }
}