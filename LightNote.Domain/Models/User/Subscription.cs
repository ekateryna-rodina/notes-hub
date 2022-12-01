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
        public Guid UserId { get;private set; }
        public virtual IEnumerable<UserProfile> Following { get { return _following; } }
        public virtual IEnumerable<UserProfile> Followers { get { return _followers; } }


    }
}

