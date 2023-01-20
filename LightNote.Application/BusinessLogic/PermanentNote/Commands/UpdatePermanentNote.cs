using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.PermanentNote.Commands
{
    public class UpdatePermanentNote : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public Guid UserProfileId { get; set; }
    }
}