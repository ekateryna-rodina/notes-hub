using System;
namespace LightNote.Domain.Models.User
{
	public class Location
	{
		private readonly List<UserProfile> _users = new List<UserProfile>();
		private Location()
		{

		}
		public Guid Id { get; private set; }
		public string Country { get; private set; }
		public string City { get; private set; }
		public IEnumerable<UserProfile> Users { get { return _users; } }
	}
}

