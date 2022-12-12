using System;
using LightNote.Application.Notes.Commands;
using LightNote.Dal;
using MediatR;

namespace LightNote.Application.Notes.CommandHandlers
{
	public class UpdateNoteContentHandler : IRequestHandler<UpdateNoteContent>
	{
        private readonly AppDbContext _context;
        public UpdateNoteContentHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateNoteContent request, CancellationToken cancellationToken)
        {
            // TODO: add null check
            var note = _context.Notes.FirstOrDefault(n => n.Id == request.Id);
            note.UpdateContent(request.Content);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}

