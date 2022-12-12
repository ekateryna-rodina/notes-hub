using System;
using LightNote.Application.Notes.Commands;
using LightNote.Dal;
using MediatR;

namespace LightNote.Application.Notes.CommandHandlers
{
	public class UpdateNoteReferenceHandler : IRequestHandler<UpdateNoteReference>
	{
        private readonly AppDbContext _context;
        public UpdateNoteReferenceHandler(AppDbContext context)
		{
            _context = context;
		}

        public async Task<Unit> Handle(UpdateNoteReference request, CancellationToken cancellationToken)
        {
            // TODO: add null check
            var note = _context.Notes.FirstOrDefault(n => n.Id == request.Id);
            note.UpdateReference(request.ReferenceId);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}

