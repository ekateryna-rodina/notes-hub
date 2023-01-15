using LightNote.Application.Helpers;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using MediatR;

namespace LightNote.Application.BusinessLogic.Tags.Commands
{
    public class CreateTag : IRequest<OperationResult<Tag>>
    {
        public string Name { get; set; } = default!;
    }
}

