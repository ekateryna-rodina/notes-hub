using System;
using LightNote.Application.BusinessLogic.References.Commands;
using LightNote.Dal;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.BusinessLogic.References.CommandHandlers
{
	public class CreateReferenceHandler : IRequestHandler<CreateReference, Reference>
	{
        private readonly AppDbContext _context;
        public CreateReferenceHandler(AppDbContext context)
		{
            _context = context;
		}

        public async  Task<Reference> Handle(CreateReference request, CancellationToken cancellationToken)
        {
            var reference = Reference.CreateReference(request.Name);
            await _context.SaveChangesAsync();
            return reference;
        }
    }
}

