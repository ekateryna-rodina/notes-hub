using System;
using LightNote.Application.Tags.Queries;
using LightNote.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Application.Tags.QueryHandlers
{
	public class GetAllTagsHandler : IRequestHandler<GetAllTags, IEnumerable<LightNote.Domain.Models.Note.Tag>>
	{
        private readonly AppDbContext _context;
		public GetAllTagsHandler(AppDbContext context)
		{
            _context = context;
		}

        public async Task<IEnumerable<LightNote.Domain.Models.Note.Tag>> Handle(GetAllTags request, CancellationToken cancellationToken)
        {
            return await _context.Tags.ToListAsync();
        }
    }
}

