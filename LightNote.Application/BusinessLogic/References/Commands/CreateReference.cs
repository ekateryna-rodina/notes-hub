using LightNote.Application.Helpers;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using MediatR;

namespace LightNote.Application.BusinessLogic.References.Commands
{
    public class CreateReference : IRequest<OperationResult<Reference>>
    {
        public string Name { get; set; } = default!;
        public bool IsLink { get; set; } = default!;
        public Guid UserProfileId { get; set; }
        public Guid NotebookId { get; set; }
        public IEnumerable<Guid> TagIds { get; set; } = default!;
    }
}

