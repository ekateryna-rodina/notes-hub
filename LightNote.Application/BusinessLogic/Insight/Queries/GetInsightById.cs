using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Insight.Queries
{
    public class GetInsightById : IRequest<OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Insight>>
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
    }
}