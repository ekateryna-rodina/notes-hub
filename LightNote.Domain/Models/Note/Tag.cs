using System;
namespace LightNote.Domain.Models.Note
{
	public class Tag
	{
		private readonly List<Note> _notes = new List<Note>();
		private Tag()
		{

		}
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public virtual ICollection<Note> Notes { get { return _notes; } }
		public static Tag CreateTag(string name) {
			return new Tag {
				Name = name
			};
		}
		public void AddNote(Note note) {
			_notes.Add(note);
		}
        public void RemoveNote(Note note)
        {
            _notes.Remove(note);
        }
    }
}

