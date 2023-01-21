using LightNote.Application.Helpers;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using MediatR;

namespace LightNote.Application.BusinessLogic.References.Queries
{
    public class GetReferencesByNotebookId : IRequest<OperationResult<IEnumerable<Reference>>>
    {

        public Guid UserProfileId { get; set; }
        public Guid NotebookId { get; set; }
    }
}

