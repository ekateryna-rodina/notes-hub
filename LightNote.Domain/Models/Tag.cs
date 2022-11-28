using System;
namespace LightNote.Domain.Models
{
	public class Tag
	{
		public Tag()
		{
			Notes = new HashSet<Note>();
		}

		public string Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<Note> Notes { get; set; }
	}
}

