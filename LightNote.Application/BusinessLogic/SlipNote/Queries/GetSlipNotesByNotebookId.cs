using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.SlipNote.Queries
{
    public class GetSlipNotesByNotebookId : IRequest<OperationResult<IEnumerable<Domain.Models.NotebookAggregate.Entities.SlipNote>>>
    {
        public Guid UserProfileId { get; set; }
        public Guid NotebookId { get; set; }
    }
}