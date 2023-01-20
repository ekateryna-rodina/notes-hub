using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.PermanentNote.Commands
{
    public class DeletePermanentNote : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
    }
}