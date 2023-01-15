using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.References.Commands
{
    public class SetTagsToReference : IRequest<OperationResult<bool>>
    {
        public Guid ReferenceId { get; set; }
        public Guid UserProfileId { get; set; }
        public List<Guid> TagIds { get; set; } = new List<Guid>();
    }
}