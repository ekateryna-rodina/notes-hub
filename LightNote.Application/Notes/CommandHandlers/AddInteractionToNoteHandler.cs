using System;
using LightNote.Application.Notes.Commands;
using LightNote.Dal;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.Notes.CommandHandlers
{
    public class AddInteractionToNoteHandler : IRequestHandler<AddInteractionToNote>
    {
        private readonly AppDbContext _context;

        public AddInteractionToNoteHandler(AppDbContext context) {
            _context = context;
        }
        public async Task<Unit> Handle(AddInteractionToNote request, CancellationToken cancellationToken)
        {
            // TODO: add null check
            var note = _context.Notes.FirstOrDefault(n => n.Id == request.Id);
            var interaction = Interaction.CreateInteraction(note.Id, request.AuthorId, request.Interaction);
            note.AddInteraction(interaction);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}

