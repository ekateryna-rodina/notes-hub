using System;
using LightNote.Application.Notes.Commands;
using LightNote.Dal;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.Notes.CommandHandlers
{
	public class RemoveCommentFromNoteHandler : IRequestHandler<RemoveCommentFromNote>
	{
        private readonly AppDbContext _context;
        public RemoveCommentFromNoteHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveCommentFromNote request, CancellationToken cancellationToken)
        {
            // TODO: add null check and grant permission 
            var note = _context.Notes.FirstOrDefault(n => n.Id == request.Id);
            var comment = _context.Comments.FirstOrDefault(c => c.Id == request.Id);
            note.RemoveComment(comment);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}

