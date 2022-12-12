using System;
using LightNote.Application.Notes.Queries;
using LightNote.Dal;
using LightNote.Domain.Models.Note;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Application.Notes.QueryHandlers
{
    public class GetAllNotesHandler : IRequestHandler<GetAllNotes, IEnumerable<Note>>
    {
        private readonly AppDbContext _context;
        public GetAllNotesHandler(AppDbContext context) {
            _context = context;
        }
        public async Task<IEnumerable<Note>> Handle(GetAllNotes request, CancellationToken cancellationToken)
        {
            return await _context.Notes.ToListAsync();
        }
    }
}

