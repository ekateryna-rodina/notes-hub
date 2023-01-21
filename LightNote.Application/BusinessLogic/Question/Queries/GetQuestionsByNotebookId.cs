using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Question.Queries
{
    public class GetQuestionsByNotebookId : IRequest<OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Entities.Question>>>
    {
        public Guid UserProfileId { get; set; }
        public Guid NotebookId { get; set; }
    }
}

