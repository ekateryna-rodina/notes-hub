using System;
using MediatR;

namespace LightNote.Application.Tags.Commands
{
	public class CreateTag : IRequest<LightNote.Domain.Models.Note.Tag>
	{
		public string Name { get; set; } = default!;
	}
}

