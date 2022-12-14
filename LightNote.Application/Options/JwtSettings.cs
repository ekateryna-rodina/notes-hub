using System;
namespace LightNote.Application.Options
{
	public class JwtSettings
	{
		public string SigningKey { get; set; } = default!;
		public string Issuer { get; set; } = default!;
		public string[] Audiences { get; set; } = default!;
    }
}

