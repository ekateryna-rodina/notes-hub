using System;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.Notes.Commands
{
	public class AddInteractionToNote : IRequest
	{
		public Guid Id { get; set; }
		public Guid AuthorId { get; set; }
		public InteractionTypes Interaction { get; set; }
	}
}

