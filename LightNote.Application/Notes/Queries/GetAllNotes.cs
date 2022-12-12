using System;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.Notes.Queries
{
	public class GetAllNotes : IRequest<IEnumerable<Note>>
	{
	}
}

