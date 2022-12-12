using System;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.Tags.Queries
{
	public class GetAllTags : IRequest<IEnumerable<LightNote.Domain.Models.Note.Tag>>
    {	
	}
}

