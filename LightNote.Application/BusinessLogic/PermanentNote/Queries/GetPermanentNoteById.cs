using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.PermanentNote.Queries
{
    public class GetPermanentNoteById : IRequest<OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote?>>
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
    }
}