using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Identity.Commands
{
    public class RefreshIdentity : IRequest<OperationResult<bool>>
    {
        public string RefreshToken { get; set; } = default!;
    }
}