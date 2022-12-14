using System;
using LightNote.Application.BusinessLogic.Notes.Commands;
using LightNote.Dal;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notes.CommandHandlers
{
	public class AddNoteLinkHandler : IRequestHandler<AddOrRemoveNoteLink>
    {

        private readonly AppDbContext _context;
        public AddNoteLinkHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddOrRemoveNoteLink request, CancellationToken cancellationToken)
        {
            // TODO: add null check
            var note = _context.Notes.FirstOrDefault(n => n.Id == request.Id);
            var link = _context.Notes.FirstOrDefault(n => n.Id == request.LinkId);
            note.AddLink(link);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}

