using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.References.Commands
{
    public class UpdateReference : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsLink { get; set; } = default!;
        public Guid UserProfileId { get; set; }
        public IEnumerable<Guid> TagIds { get; set; } = new List<Guid>();
    }
}