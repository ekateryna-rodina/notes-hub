using System;
using LightNote.Application.Notes.Commands;
using LightNote.Dal;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.Notes.CommandHandlers
{
	public class UpdateNoteTitleHandler : IRequestHandler<UpdateNoteTitle>
    {
        private readonly AppDbContext _context;
        public UpdateNoteTitleHandler(AppDbContext context)
		{
			_context = context;
		}

        public async Task<Unit> Handle(UpdateNoteTitle request, CancellationToken cancellationToken)
        {
            // TODO: add null check
            var note = _context.Notes.FirstOrDefault(n => n.Id == request.Id);
            note.UpdateTitle(request.Title);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}

