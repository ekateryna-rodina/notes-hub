using System;
using MediatR;

namespace LightNote.Application.BusinessLogic.Tags.Commands
{
	public class CreateTag : IRequest<Domain.Models.Note.Tag>
	{
		public string Name { get; set; } = default!;
	}
}

