using System;
using LightNote.Domain.Models.User;

namespace LightNote.Api.Contracts.User.Response
{
	public record BasicUserInfo
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhotoUrl { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
    }
}

