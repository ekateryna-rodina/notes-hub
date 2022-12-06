using System;
namespace LightNote.Domain.Models.Note
{
	public class Reference
	{
		private readonly List<Note> _notes = new List<Note>();
		private Reference()
		{
		}

		public Guid Id { get; private set; }
		public string Name { get; set; }
		public IEnumerable<Note> Notes { get { return _notes; } }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public static Reference CreateReference(string name) {
			return new Reference {
				Name = name,
				CreatedAt = DateTime.UtcNow
			};
		}
		public void UpdateReference(string name) {
			Name = name;
			UpdatedAt = DateTime.UtcNow;
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

