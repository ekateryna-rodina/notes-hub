using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Question.Queries
{
    public class GetQuestionById : IRequest<OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Question?>>
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
    }
}