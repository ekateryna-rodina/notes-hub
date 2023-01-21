using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Question.Commands
{
    public class CreateQuestion : IRequest<OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Question>>
    {
        public string Content { get; set; } = default!;
        public Guid UserProfileId { get; set; }
        public Guid NotebookId { get; set; }
        public Guid? InsightId { get; set; }
        public Guid? PermanentNoteId { get; set; }
    }
}