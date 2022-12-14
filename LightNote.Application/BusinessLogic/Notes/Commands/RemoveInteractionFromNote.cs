using System;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notes.Commands
{
	public class RemoveInteractionFromNote : IRequest
	{
		public Guid Id { get; set; }
		public Guid InteractionId { get; set; }
	}
}

