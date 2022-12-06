using System;
using LightNote.Domain.Models.Note;

namespace LightNote.Domain.Models.User
{
	public class Subscription
	{
        private Subscription()
        {

        }
        private readonly List<UserProfile> _following = new List<UserProfile>();
        private readonly List<UserProfile> _followers = new List<UserProfile>();

        public virtual IEnumerable<UserProfile> Following { get { return _following; } }
        public virtual IEnumerable<UserProfile> Followers { get { return _followers; } }

        public void AddFollowing(UserProfile userProfile)
        {
            _following.Add(userProfile);
        }
        public void RemoveFollowing(UserProfile userProfile)
        {
            _following.Remove(userProfile);
        }
        public void AddFollowers(UserProfile userProfile)
        {
            _followers.Add(userProfile);
        }
        public void RemoveFollowers(UserProfile userProfile)
        {
            _followers.Remove(userProfile);
        }

    }
}

