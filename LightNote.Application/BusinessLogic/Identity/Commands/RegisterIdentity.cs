using LightNote.Application.Helpers;
using LightNote.Application.Models;
using MediatR;

namespace LightNote.Application.BusinessLogic.Identity.Commands
{
    public class RegisterIdentity : IRequest<OperationResult<AuthenticatedResponse>>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string PhotoUrl { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
    }
}

