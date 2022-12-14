using System;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notes.Commands
{
	public class AddCommentToNote : IRequest
	{
		public Guid Id { get; set; }
		public Guid AuthorId { get; set; }
		public Guid? CommentedId { get; set; }
		public string Text { get; set; } = default!;
	}
}

