using System;
namespace LightNote.Domain.Models.Note
{
	public class Comment
	{
		private readonly List<CommentInteraction> _interactions = new List<CommentInteraction>();

        private Comment()
		{
				
		}
		public Guid Id {get;private set;}
		public Guid NoteId { get; private set; }
		public Guid UserProfileId { get; private set; }
		public string Text { get; private set; }
		public DateTime CreatedAt { get; private set; }
		public DateTime? UpdatedAt { get; private set; }
        public virtual IEnumerable<CommentInteraction> Interactions { get { return _interactions; } }

        public static Comment CreateComment(Guid noteId, Guid userProfileId, string text) {
			return new Comment
			{
				NoteId = noteId,
				UserProfileId = userProfileId,
				Text = text,
				CreatedAt = DateTime.UtcNow
			};
		}
        public void UpdateComment(string text)
        {
			Text = text;
			UpdatedAt = DateTime.UtcNow;
        }

		public void AddInteraction(CommentInteraction interaction) {
			_interactions.Add(interaction);
		}
        public void RemoveInteraction(CommentInteraction interaction)
        {
            _interactions.Remove(interaction);
        }
    }
}

