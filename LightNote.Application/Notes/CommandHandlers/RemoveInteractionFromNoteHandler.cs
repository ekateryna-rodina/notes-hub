using System;
using LightNote.Application.Notes.Commands;
using LightNote.Dal;
using MediatR;

namespace LightNote.Application.Notes.CommandHandlers
{
	public class RemoveInteractionFromNoteHandler : IRequestHandler<RemoveInteractionFromNote>
	{
        private readonly AppDbContext _context;
		public RemoveInteractionFromNoteHandler(AppDbContext context)
		{
            _context = context;
		}

        public async Task<Unit> Handle(RemoveInteractionFromNote request, CancellationToken cancellationToken)
        {
            // TODO: add null check and grant permission 
            var note = _context.Notes.FirstOrDefault(n => n.Id == request.Id);
            var interaction = _context.Interactions.FirstOrDefault(c => c.Id == request.Id);
            note.RemoveInteraction(interaction);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}

