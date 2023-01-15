using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notebook.Queries
{
    public class GetNotebookById : IRequest<OperationResult<LightNote.Domain.Models.NotebookAggregate.Notebook?>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}