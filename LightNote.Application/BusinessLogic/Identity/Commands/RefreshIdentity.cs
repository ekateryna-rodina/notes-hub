using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.Helpers;
using LightNote.Application.Models;
using MediatR;

namespace LightNote.Application.BusinessLogic.Identity.Commands
{
    public class RefreshIdentity : IRequest<OperationResult<AuthenticatedResponse>>
    {
        public string RefreshToken { get; set; } = default!;
    }
}