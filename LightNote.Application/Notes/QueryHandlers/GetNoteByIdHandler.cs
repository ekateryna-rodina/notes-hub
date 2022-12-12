using System;
using LightNote.Application.Notes.Queries;
using LightNote.Dal;
using LightNote.Domain.Models.Note;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Application.Notes.QueryHandlers
{
	public class GetNoteByIdHandler : IRequestHandler<GetNoteById, Note>
	{
        private readonly AppDbContext _context;
        public GetNoteByIdHandler(AppDbContext context)
		{
            _context = context;
		}

        public async Task<Note> Handle(GetNoteById request, CancellationToken cancellationToken)
        {
            // TODO: null referenece
            return await _context.Notes.FirstOrDefaultAsync(n => n.Id == request.Id);
        }
    }
}

