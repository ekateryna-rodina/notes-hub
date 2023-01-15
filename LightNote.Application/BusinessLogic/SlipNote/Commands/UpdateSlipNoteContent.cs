using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.SlipNote.Commands
{
    public class UpdateSlipNoteContent : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
        public string Content { get; set; } = default!;
    }
}