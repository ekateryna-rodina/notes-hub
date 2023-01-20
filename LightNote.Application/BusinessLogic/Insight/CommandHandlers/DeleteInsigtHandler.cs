using LightNote.Application.BusinessLogic.Insight.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.Insight.CommandHandlers
{
    public class DeleteInsightHandler : IRequestHandler<DeleteInsight, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInsightHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(DeleteInsight request, CancellationToken cancellationToken)
        {

            var insight = await _unitOfWork.InsightRepository.GetByID(request.Id);
            if (insight == null)
            {
                return OperationResult<bool>.CreateFailure(new[] { new ResourceNotFoundException(nameof(Insight)) });
            }
            if (insight.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<bool>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            try
            {
                _unitOfWork.InsightRepository.Delete(insight);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(new[] { ex });
            }
            return OperationResult<bool>.CreateSuccess(true);
        }
    }
}