using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Insight.Commands
{
    public class CreateInsight : IRequest<OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Insight>>
    {
        public string Content { get; set; } = default!;
        public Guid UserProfileId { get; set; }
        public Guid NotebookId { get; set; }
        public IEnumerable<Guid> PermanentNoteIds { get; set; } = default!;
    }
}