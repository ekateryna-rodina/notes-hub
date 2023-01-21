using LightNote.Application.BusinessLogic.Insight.Queries;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.Insight.QueryHandlers
{
    public class GetInsightsByNotebookIdHandler : IRequestHandler<GetInsightsByNotebookId, OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Entities.Insight>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInsightsByNotebookIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Entities.Insight>>> Handle(GetInsightsByNotebookId request, CancellationToken cancellationToken)
        {
            var insights = await _unitOfWork.InsightRepository.Get(s => s.NotebookId.Value == request.NotebookId && s.UserProfileId.Value == request.UserProfileId);
            return OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Entities.Insight>>.CreateSuccess(insights);
        }
    }
}