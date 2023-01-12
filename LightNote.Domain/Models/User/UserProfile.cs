using System;
namespace LightNote.Domain.Models.User
{
    public class UserProfile
    {
        private readonly List<Note.Note> _notes = new List<Note.Note>();
        private UserProfile()
        {

        }
        public Guid Id { get; private set; }
        public string IdentityId { get; private set; }
        public BasicUserInfo BasicUserInfo { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public IEnumerable<Note.Note> Notes { get { return _notes; } }
        public Subscription Subscription { get; private set; }

        public static UserProfile CreateUserProfile(string identityId, BasicUserInfo basicInfo)
        {
            return new UserProfile
            {
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
        public void Updatelocation(string country, string city)
        {
            BasicUserInfo.Updatelocation(country, city);
        }
        public void AddFollowing(UserProfile userProfile)
        {
            Subscription.AddFollowing(userProfile);
        }
        public void RemoveFollowing(UserProfile userProfile)
        {
            Subscription.RemoveFollowing(userProfile);
        }
        public void AddFollowers(UserProfile userProfile)
        {
            Subscription.AddFollowers(userProfile);
        }
        public void RemoveFollowers(UserProfile userProfile)
        {
            Subscription.RemoveFollowers(userProfile);
        }
    }
}

