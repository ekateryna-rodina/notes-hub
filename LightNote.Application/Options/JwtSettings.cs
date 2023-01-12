using System;
namespace LightNote.Application.Options
{
    public class JwtSettings
    {
        public double AccessTokenExpiration { get; set; } = default!;
        public double RefreshTokenExpiration { get; set; } = default!;
        public string AccessSigningKey { get; set; } = default!;
        public string RefreshSigningKey { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string[] Audiences { get; set; } = default!;
    }
}

