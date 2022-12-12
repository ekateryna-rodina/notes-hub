using System;
using LightNote.Application.Tags.Commands;
using LightNote.Dal;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.Tags.CommandHandlers
{
	public class CreateTagHandler : IRequestHandler<CreateTag, LightNote.Domain.Models.Note.Tag>
	{
        private readonly AppDbContext _context;
        public CreateTagHandler(AppDbContext context)
		{
			_context = context;
		}

        public async Task<LightNote.Domain.Models.Note.Tag> Handle(CreateTag request, CancellationToken cancellationToken)
        {
            var tag = LightNote.Domain.Models.Note.Tag.CreateTag(request.Name);
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag;
        }
    }
}

