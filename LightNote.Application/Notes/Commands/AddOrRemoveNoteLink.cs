using System;
using MediatR;

namespace LightNote.Application.Notes.Commands
{
	public class AddOrRemoveNoteLink : IRequest
	{
		public Guid Id { get; set; }
		public Guid LinkId { get; set; }
	}
}

