using System;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notes.Commands
{
	public class DeleteNote : IRequest
	{
		public Guid Id { get; set; }
	}
}

