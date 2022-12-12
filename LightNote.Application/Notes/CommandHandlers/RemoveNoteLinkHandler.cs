using System;
using LightNote.Application.Notes.Commands;
using LightNote.Dal;
using MediatR;

namespace LightNote.Application.Notes.CommandHandlers
{
	public class RemoveNoteLinkHandler : IRequestHandler<AddOrRemoveNoteLink>
    {
        private readonly AppDbContext _context;
        public RemoveNoteLinkHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddOrRemoveNoteLink request, CancellationToken cancellationToken)
        {
            // TODO: add null check
            var note = _context.Notes.FirstOrDefault(n => n.Id == request.Id);
            var link = _context.Notes.FirstOrDefault(n => n.Id == request.LinkId);
            note.RemoveLink(link);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}

