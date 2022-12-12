using System;
using LightNote.Application.Notes.Commands;
using LightNote.Dal;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.Notes.CommandHandlers
{
	public class AddCommentToNoteHandler : IRequestHandler<AddCommentToNote>
	{
        private readonly AppDbContext _context;
        public AddCommentToNoteHandler(AppDbContext context)
		{
            _context = context;
		}

        public async Task<Unit> Handle(AddCommentToNote request, CancellationToken cancellationToken)
        {
            // TODO: add null check
            var note = _context.Notes.FirstOrDefault(n => n.Id == request.Id);
            var comment = Comment.CreateComment(note.Id, request.AuthorId, request.Text, request.CommentedId);
            note.AddComment(comment);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}

