using System;
using LightNote.Domain.Models.User;

namespace LightNote.Domain.Models.Note
{
	public class Comment
    {
		private readonly List<Interaction> _interactions = new List<Interaction>();
        private readonly List<Comment> _comments = new List<Comment>();

        private Comment()
		{
				
		}

		public Guid Id {get;private set;}
		public Guid NoteId { get; private set; }
        public Note Note { get; private set; }
        public Guid? CommentId { get; private set; }
        public Comment Commented { get; private set; }
        public Guid UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public string Text { get; private set; }
		public DateTime CreatedAt { get; private set; }
		public DateTime? UpdatedAt { get; private set; }
        // TODO: Do we actually need these? 
        public virtual IEnumerable<Interaction> Interactions { get { return _interactions; } }
        public virtual IEnumerable<Comment> Comments { get { return _comments; } }

        public static Comment CreateComment(Guid noteId, Guid userProfileId, string text, Guid? commentedId) {
			return new Comment
            {
				NoteId = noteId,
				UserProfileId = userProfileId,
				Text = text,
				CreatedAt = DateTime.UtcNow,
                CommentId = commentedId
			};
		}
        public void UpdateComment(string text)
        {
			Text = text;
			UpdatedAt = DateTime.UtcNow;
        }

		public void AddInteraction(Interaction interaction) {
			_interactions.Add(interaction);
		}
        public void RemoveInteraction(Interaction interaction)
        {
            _interactions.Remove(interaction);
        }
        public void RemoveComment(Comment comment)
        {
            _comments.Remove(comment);
        }
    }
}

