using System;
using LightNote.Application.Helpers;
using LightNote.Application.Models;
using MediatR;

namespace LightNote.Application.BusinessLogic.Identity.Commands
{
    public class LoginIdentity : IRequest<OperationResult<AuthenticatedResponse>>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}

