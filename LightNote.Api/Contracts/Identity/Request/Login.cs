using System;
using System.ComponentModel.DataAnnotations;

namespace LightNote.Api.Contracts.Identity.Request
{
	public class Login
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; } = default!;

		[Required]
		public string Password { get; set; } = default!;
	}
}

