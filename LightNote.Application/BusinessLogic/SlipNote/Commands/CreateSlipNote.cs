using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.SlipNote.Commands
{
    public class CreateSlipNote : IRequest<OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.SlipNote>>
    {
        public string Content { get; set; } = default!;
        public Guid UserProfileId { get; set; }
        public Guid NotebookId { get; set; }
        public IEnumerable<Guid> ReferenceIds { get; set; } = default!;
    }
}