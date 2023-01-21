using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Question.Commands
{
    public class DeleteQuestion : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
    }
}