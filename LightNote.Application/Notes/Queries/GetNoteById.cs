using System;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.Notes.Queries
{
	public class GetNoteById : IRequest<Note>
	{
		public Guid Id { get; set; }
	}
}

