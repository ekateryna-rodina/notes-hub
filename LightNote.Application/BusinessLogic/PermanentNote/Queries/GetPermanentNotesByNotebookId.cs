using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.PermanentNote.Queries
{
    public class GetPermanentNotesByNotebookId : IRequest<OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote>>>
    {
        public Guid UserProfileId { get; set; }
        public Guid NotebookId { get; set; }
    }
}