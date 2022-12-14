using System;
namespace LightNote.Domain.Models.User
{
	public class BasicUserInfo
	{
        private BasicUserInfo()
        {

        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhotoUrl { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }

        public static BasicUserInfo CreateBasicUserInfo(string firstName, string lastName, string photoUrl, string country, string city) {
            return new BasicUserInfo {
                FirstName = firstName,
                LastName = lastName,
                PhotoUrl = photoUrl,
                Country = country,
                City = city
            };
        }
      
        public void UpdateFirstName(string firstName) {
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

