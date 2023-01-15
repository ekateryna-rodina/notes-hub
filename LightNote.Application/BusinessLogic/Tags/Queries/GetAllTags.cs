using LightNote.Application.Helpers;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using MediatR;

namespace LightNote.Application.BusinessLogic.Tags.Queries
{
    public class GetAllTags : IRequest<OperationResult<IEnumerable<Tag>>>
    {
    }
}

