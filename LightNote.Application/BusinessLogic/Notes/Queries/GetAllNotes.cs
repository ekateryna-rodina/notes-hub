using System;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notes.Queries
{
	public class GetAllNotes : IRequest<IEnumerable<Note>>
	{
	}
}

