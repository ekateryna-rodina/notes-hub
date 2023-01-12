using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.BusinessLogic.Identity.Commands;
using LightNote.Application.Contracts;
using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Identity.CommandHandlers
{
    public class RefreshIdentityHandler : IRequestHandler<RefreshIdentity, OperationResult<bool>>
    {
        private readonly ITokenValidator _tokenValidator;
        public RefreshIdentityHandler(ITokenValidator tokenValidator)
        {
            _tokenValidator = tokenValidator;
        }
        public Task<OperationResult<bool>> Handle(RefreshIdentity request, CancellationToken cancellationToken)
        {
            var isValid = _tokenValidator.Validate(request.RefreshToken);
            return Task.FromResult(OperationResult<bool>.CreateSuccess(isValid));
        }
    }
}