using System;
using LightNote.Domain.Models.User;

namespace LightNote.Domain.Models.Note
{
	public class NoteInteraction
    {
		private NoteInteraction()
		{
		}

        public Guid Id { get; private set; }
        public Guid NoteId { get; private set; }
        public Guid? UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public InteractionTypes InteractionType { get; private set; }
        public static NoteInteraction CreateNoteInteraction(Guid noteId, Guid userProfileId, InteractionTypes interactionType)
        {
            return new NoteInteraction
            {
                NoteId = noteId,
                UserProfileId = userProfileId,
                InteractionType = interactionType
            };
        }
    }
}

