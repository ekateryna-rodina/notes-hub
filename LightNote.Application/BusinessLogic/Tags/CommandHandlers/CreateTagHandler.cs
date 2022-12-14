using System;
using LightNote.Application.BusinessLogic.Tags.Commands;
using LightNote.Dal;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.BusinessLogic.Tags.CommandHandlers
{
	public class CreateTagHandler : IRequestHandler<CreateTag, Tag>
	{
        private readonly AppDbContext _context;
        public CreateTagHandler(AppDbContext context)
		{
			_context = context;
		}

        public async Task<Tag> Handle(CreateTag request, CancellationToken cancellationToken)
        {
            var tag = Tag.CreateTag(request.Name);
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag;
        }
    }
}

