using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Insight.Commands
{
    public class DeleteInsight : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
    }
}