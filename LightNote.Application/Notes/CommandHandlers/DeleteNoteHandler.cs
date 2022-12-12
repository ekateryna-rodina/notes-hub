using System;
using LightNote.Application.Notes.Commands;
using LightNote.Dal;
using LightNote.Domain.Models.Note;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Application.Notes.CommandHandlers
{
	public class DeleteNoteHandler : IRequestHandler<UpdateNoteTitle>
	{
        private readonly AppDbContext _context;
        public DeleteNoteHandler(AppDbContext context)
		{
			_context = context;
		}

        public async Task<Unit> Handle(UpdateNoteTitle request, CancellationToken cancellationToken)
        {
            // TODO: add null check
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == request.Id);
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}

