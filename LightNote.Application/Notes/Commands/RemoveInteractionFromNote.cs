using System;
using MediatR;

namespace LightNote.Application.Notes.Commands
{
	public class RemoveInteractionFromNote : IRequest
	{
		public Guid Id { get; set; }
		public Guid InteractionId { get; set; }
	}
}

