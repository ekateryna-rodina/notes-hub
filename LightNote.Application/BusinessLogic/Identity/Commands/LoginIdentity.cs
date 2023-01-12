using System;
using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Identity.Commands
{
    public class LoginIdentity : IRequest<OperationResult<(string, string)>>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}

