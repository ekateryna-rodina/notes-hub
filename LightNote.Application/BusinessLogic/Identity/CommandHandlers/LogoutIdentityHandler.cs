using LightNote.Application.BusinessLogic.Identity.Commands;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.Identity.CommandHandlers
{
    public class LogoutIdentityHandler : IRequestHandler<LogoutIdentity, OperationResult<bool>>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public LogoutIdentityHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public Task<OperationResult<bool>> Handle(LogoutIdentity request, CancellationToken cancellationToken)
        {
            _refreshTokenRepository.RemoveRefreshTokensByUserIdAsync(request.UserId);
            return Task.FromResult<OperationResult<bool>>(OperationResult<bool>.CreateSuccess(true));
        }
    }
}