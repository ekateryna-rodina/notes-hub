using System;
namespace LightNote.Domain.Models
{
	public class Note
	{
		public Note() {
			Tags = new HashSet<Tag>();
		}

		public string Id { get; set; }
		public string Title { get; set; }
        public string Body { get; set; }
        public bool IsPublic { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}

