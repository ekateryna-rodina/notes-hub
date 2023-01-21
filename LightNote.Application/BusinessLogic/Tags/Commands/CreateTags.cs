using System.Collections.ObjectModel;
using LightNote.Application.Helpers;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using MediatR;

namespace LightNote.Application.BusinessLogic.Tags.Commands
{
    public class CreateTags : IRequest<OperationResult<IEnumerable<Tag>>>
    {
        public IEnumerable<string> Names { get; set; } = default!;
    }
}

