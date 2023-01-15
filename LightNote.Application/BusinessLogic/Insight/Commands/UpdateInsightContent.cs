using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Insight.Commands
{
    public class UpdateInsightContent : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public Guid UserProfileId { get; set; }
    }
}