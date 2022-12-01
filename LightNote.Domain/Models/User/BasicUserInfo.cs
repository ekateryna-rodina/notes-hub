using System;
namespace LightNote.Domain.Models.User
{
	public class BasicUserInfo
	{
        private BasicUserInfo()
        {

        }
        public Guid Id { get; private set; }
        public Guid IdentityId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhotoUrl { get; private set; }
        public Location Location { get; private set; }


      
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
        public void Updatelocation(Location location)
        {
            Location = location;
        }
    }
}

