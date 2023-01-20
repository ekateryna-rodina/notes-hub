using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Insight.Queries
{
    public class GetInsightsByNotebookId : IRequest<OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Entities.Insight>>>
    {
        public Guid UserProfileId { get; set; }
        public Guid NotebookId { get; set; }
    }
}