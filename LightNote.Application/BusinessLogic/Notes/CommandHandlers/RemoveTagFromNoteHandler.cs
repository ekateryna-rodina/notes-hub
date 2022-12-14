using System;
using LightNote.Application.BusinessLogic.Notes.Commands;
using LightNote.Dal;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notes.CommandHandlers
{
	public class RemoveTagFromNoteHandler : IRequestHandler<AddOrRemoveTag>
	{
        private readonly AppDbContext _context;
        public RemoveTagFromNoteHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddOrRemoveTag request, CancellationToken cancellationToken)
        {
            // TODO: add null check
            var note = _context.Notes.FirstOrDefault(n => n.Id == request.Id);
            var tag = _context.Tags.FirstOrDefault(n => n.Id == request.TagId);
            note.RemoveTag(tag);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}

