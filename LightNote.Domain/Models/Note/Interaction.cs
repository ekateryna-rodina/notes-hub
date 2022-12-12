using System;
using LightNote.Domain.Models.User;

namespace LightNote.Domain.Models.Note
{
	public class Interaction
    {
		private Interaction()
		{
		}

        public Guid Id { get; private set; }
        public Guid NoteId { get; private set; }
        public Note Note { get; set; }
        public Guid? CommentId { get; private set; }
        public Comment Comment { get; set; }
        public Guid UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public InteractionTypes InteractionType { get; private set; }
        public static Interaction CreateInteraction(Guid noteId, Guid userProfileId, InteractionTypes interactionType)
        {
            return new Interaction
            {
                NoteId = noteId,
                UserProfileId = userProfileId,
                InteractionType = interactionType
            };
        }
    }
}

