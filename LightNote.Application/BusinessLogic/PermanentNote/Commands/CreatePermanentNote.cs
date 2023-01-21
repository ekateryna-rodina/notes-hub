using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.PermanentNote.Commands
{
    public class CreatePermanentNote : IRequest<OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote>>
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public Guid UserProfileId { get; set; }
        public Guid NotebookId { get; set; }
        public IEnumerable<Guid> SlipNoteIds = default!;
    }
}