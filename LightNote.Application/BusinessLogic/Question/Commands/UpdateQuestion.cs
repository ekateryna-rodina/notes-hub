using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Question.Commands
{
    public class UpdateQuestion : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public Guid UserProfileId { get; set; }
    }
}