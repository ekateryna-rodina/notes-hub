using System;
using LightNote.Application.References.Queries;
using LightNote.Dal;
using LightNote.Domain.Models.Note;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Application.References.QueryHandlers
{
	public class GetAllReferencesHandler : IRequestHandler<GetAllReferences, IEnumerable<Reference>>
	{
        private readonly AppDbContext _context;
        public GetAllReferencesHandler(AppDbContext context)
		{
            _context = context;
		}

        public async Task<IEnumerable<Reference>> Handle(GetAllReferences request, CancellationToken cancellationToken)
        {
            // TODO: get users specific references
            return await _context.References.ToListAsync();
        }
    }
}

