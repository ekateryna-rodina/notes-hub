using System;
using MediatR;

namespace LightNote.Application.Notes.Commands
{
	public class DeleteNote : IRequest
	{
		public Guid Id { get; set; }
	}
}

