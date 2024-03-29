﻿using System.ComponentModel.DataAnnotations;

namespace LightNote.Api.Contracts.Identity.Request
{
	public class RegisterRequest
	{
        [Required]
        public string FirstName { get; set; } = default!;
        [Required]
        public string LastName { get; set; } = default!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
        [Required]
        public string Country { get; set; } = default!;
        [Required]
        public string City { get; set; } = default!;
        [Required]
        public string PhotoUrl { get; set; } = default!;
    }
}

