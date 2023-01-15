using System;
using LightNote.Domain.Models.UserProfile.ValueObjects;

namespace LightNote.Domain.Models.UserProfile
{
    public class UserProfile
    {
        private UserProfile()
        {

        }
        public UserProfileId Id { get; private set; }
        public string IdentityId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhotoUrl { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static UserProfile CreateUserProfile(string identityId,
            string firstName, string lastName, string photoUrl, string country, string city)
        {
            return new UserProfile
            {
                Id = UserProfileId.Create(),
                FirstName = firstName,
                LastName = lastName,
                PhotoUrl = photoUrl,
                Country = country,
                City = city,
                IdentityId = identityId,
                CreatedAt = DateTime.UtcNow
            };
        }
        public void UpdateFirstName(string firstName)
        {
            FirstName = firstName;
        }
        public void UpdateLastName(string lastName)
        {
            LastName = lastName;
        }
        public void UpdatePhotoUrl(string photoUrl)
        {
            PhotoUrl = photoUrl;
        }
        public void Updatelocation(string country, string city)
        {
            Country = country;
            City = city;
        }
    }
}

