using System;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notes.Commands
{
	public class UpdateNoteTitle : IRequest
	{
		public Guid Id { get; set; }
        public string Title { get; set; } = default!;
    }
}

