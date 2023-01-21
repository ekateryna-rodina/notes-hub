using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notebook.Commands
{
    public class UpdateNotebook : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
        public string Title { get; set; } = default!;
    }
}