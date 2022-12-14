using System;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notes.Queries
{
	public class GetNoteById : IRequest<Note>
	{
		public Guid Id { get; set; }
	}
}

