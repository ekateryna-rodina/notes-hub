using System;
using MediatR;

namespace LightNote.Application.Notes.Commands
{
	public class UpdateNoteContent : IRequest
	{
		public Guid Id { get; set; }
        public string Content { get; set; } = default!;
    }
}

