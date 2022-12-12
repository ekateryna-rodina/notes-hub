﻿using System;
using LightNote.Application.Notes.Commands;
using LightNote.Dal;
using MediatR;

namespace LightNote.Application.Notes.CommandHandlers
{
	public class AddTagToNoteHandler : IRequestHandler<AddOrRemoveTag>
    {
        private readonly AppDbContext _context;
        public AddTagToNoteHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddOrRemoveTag request, CancellationToken cancellationToken)
        {
            // TODO: add null check
            var note = _context.Notes.FirstOrDefault(n => n.Id == request.Id);
            var tag = _context.Tags.FirstOrDefault(n => n.Id == request.TagId);
            note.AddTag(tag);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}

