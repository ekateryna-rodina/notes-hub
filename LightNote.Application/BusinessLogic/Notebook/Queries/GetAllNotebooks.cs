using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notebook.Queries
{
    public class GetAllNotebooks : IRequest<OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Notebook>>>
    {
        public Guid UserProfileId { get; set; }
    }
}