using System;
using LightNote.Application.BusinessLogic.Notes.Commands;
using LightNote.Dal;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notes.CommandHandlers
{
	public class CreateNoteHandler : IRequestHandler<CreateNote, Note>
	{
        private readonly AppDbContext _context;
		public CreateNoteHandler(AppDbContext context)
		{
            _context = context;
		}

        public async Task<Note> Handle(CreateNote request, CancellationToken cancellationToken)
        {
            // TODO: apply userId
            var note = Note.CreateNote(Guid.Empty, request.Title, request.Content, request.IsPublic);
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }
    }
}

