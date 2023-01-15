using System;
using LightNote.Domain.Models.Notebook.ValueObjects;
using LightNote.Domain.Models.UserProfile.ValueObjects;

namespace LightNote.Domain.Models.Notebook.Entities
{
	public class Tag
	{
		public TagId Id { get; private set; }
		public string Name { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public LightNote.Domain.Models.UserProfile.UserProfile UserProfiled { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
    }
}

