using System;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notes.Commands
{
	public class AddOrRemoveTag : IRequest
	{
		public Guid Id { get; set; }
		public Guid TagId { get; set; }
	}
}

