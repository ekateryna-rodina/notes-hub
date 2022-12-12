using System;
using MediatR;

namespace LightNote.Application.Notes.Commands
{
	public class UpdateNoteTitle : IRequest
	{
		public Guid Id { get; set; }
        public string Title { get; set; } = default!;
    }
}

