using LightNote.Application.Helpers;
using MediatR;
namespace LightNote.Application.BusinessLogic.Notebook.Commands
{
    public class CreateNotebook : IRequest<OperationResult<LightNote.Domain.Models.NotebookAggregate.Notebook>>
    {
        public string Title { get; set; } = default!;
        public Guid UserProfileId { get; set; } = default!;
    }
}