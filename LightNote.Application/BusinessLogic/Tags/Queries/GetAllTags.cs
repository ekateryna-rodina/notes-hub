using System;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.BusinessLogic.Tags.Queries
{
	public class GetAllTags : IRequest<IEnumerable<Tag>>
    {	
	}
}

