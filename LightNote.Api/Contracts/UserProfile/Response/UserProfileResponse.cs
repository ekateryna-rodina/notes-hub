using System;
namespace LightNote.Api.Contracts.User.Response
{
    public class UserProfileResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhotoUrl { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
    }
}

