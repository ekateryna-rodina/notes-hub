using System;
namespace LightNote.Api.Contracts.Identity.Response
{
    public class AuthenticationResponse
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}

