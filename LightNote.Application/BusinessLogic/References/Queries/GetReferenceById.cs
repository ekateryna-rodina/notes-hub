using LightNote.Application.Helpers;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using MediatR;

namespace LightNote.Application.BusinessLogic.References.Queries
{
    public class GetReferenceById : IRequest<OperationResult<Reference?>>
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
    }
}