using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.SlipNote.Queries
{
    public class GetSlipnoteById : IRequest<OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.SlipNote?>>
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
    }
}