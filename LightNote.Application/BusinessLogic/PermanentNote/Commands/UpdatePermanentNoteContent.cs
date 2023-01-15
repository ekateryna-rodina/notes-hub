using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.PermanentNote.Commands
{
    public class UpdatePermanentNoteContent : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public Guid UserProfileId { get; set; }
    }
}