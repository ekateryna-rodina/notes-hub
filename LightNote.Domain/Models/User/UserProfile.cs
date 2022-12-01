using System;
namespace LightNote.Domain.Models.User
{
	public class UserProfile
	{
        private readonly List<UserProfile> _following = new List<UserProfile>();
        private readonly List<UserProfile> _followers = new List<UserProfile>();
        private UserProfile()
        {

        }
        public Guid Id { get; private set; }
		public string IdentityId { get; private set; }
		public BasicUserInfo BasicUserInfo { get; private set; }
		public DateTime CreatedAt { get; private set; }
		public DateTime UpdatedAt { get; private set; }
        public virtual IEnumerable<UserProfile> Following { get { return _following; } }
        public virtual IEnumerable<UserProfile> Followers { get { return _followers; } }
        public static UserProfile CreateUserProfile(string identityId, BasicUserInfo basicInfo) {
            return new UserProfile {
                IdentityId = identityId,
                BasicUserInfo = basicInfo,
                CreatedAt = DateTime.UtcNow
            };
        }
        public void UpdateFirstName(string firstName)
        {
            BasicUserInfo.UpdateFirstName(firstName);
        }
        public void UpdateLastName(string lastName)
        {
            BasicUserInfo.UpdateLastName(lastName);
        }
        public void UpdatePhotoUrl(string photoUrl)
        {
            BasicUserInfo.UpdatePhotoUrl(photoUrl);
        }
        public void Updatelocation(Location location)
        {
            BasicUserInfo.Updatelocation(location);
        }
    }
}

