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
        public LoginIdentityHandler(IToken tokenService, UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)        {
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<string>> Handle(LoginIdentity request, CancellationToken cancellationToken)
        {
            // check if user exists
            var existingIdentity = await _userManager.FindByEmailAsync(request.Email);
            if (existingIdentity == null) {
                var operationResult = OperationResult<string>.CreateFailure(new[] {new ResourceNotFoundException("User is not registered") });
                return operationResult;
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(existingIdentity, request.Password);
            if (!isPasswordValid) {
                var operationResult = OperationResult<string>.CreateFailure(new[] { new IncorrectPasswordException("Login is incorrect") });
                return operationResult;
            }

            var userProfiles = await _unitOfWork.UserRepository.Get(u => u.IdentityId == existingIdentity.Id);

            if (userProfiles == null || userProfiles.FirstOrDefault() == null) {
                var operationResult = OperationResult<string>.CreateFailure(new[] { new ResourceNotFoundException("User profile does not exist") });
                return operationResult;
            }

            // generate token
            var token = _tokenService.GenerateJwtToken(existingIdentity.Id, userProfiles.FirstOrDefault()!.Id, request.Email);
            return OperationResult<string>.CreateSuccess(token);
        }
    }
}

