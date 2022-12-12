using System;
using LightNote.Application.Notes.Commands;
using LightNote.Dal;
using MediatR;

namespace LightNote.Application.Notes.CommandHandlers
{
	public class UpdateIsPublicNoteHandler : IRequestHandler<UpdateIsPublicNote>
	{
        private readonly AppDbContext _context;
        public UpdateIsPublicNoteHandler(AppDbContext context)
		{
            _context = context;
		}

        public async Task<Unit> Handle(UpdateIsPublicNote request, CancellationToken cancellationToken)
        {
            // TODO: add null check
            var note = _context.Notes.FirstOrDefault(n => n.Id == request.Id);
            note.UpdateIsPublic(request.IsPublic);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}

