using System;
using MediatR;

namespace LightNote.Application.Notes.Commands
{
	public class UpdateNoteReference : IRequest
	{
        public Guid Id { get; set; }
        public Guid ReferenceId { get; set; } = default!;
    }
}

