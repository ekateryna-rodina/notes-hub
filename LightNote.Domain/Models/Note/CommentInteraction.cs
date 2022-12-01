using System;
using LightNote.Domain.Models.User;

namespace LightNote.Domain.Models.Note
{
	public class CommentInteraction
	{
		private CommentInteraction()
		{
		}

        public Guid Id { get; private set; }
        public Guid CommentId { get; private set; }
        public Guid? UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public InteractionTypes InteractionType { get; private set; }

        public static CommentInteraction CreateCommentInteraction(Guid commentId, Guid userProfileId, InteractionTypes interactionType)
        {
            return new CommentInteraction
            {
                CommentId = commentId,
                UserProfileId = userProfileId,
                InteractionType = interactionType
            };
        }
    }
}

