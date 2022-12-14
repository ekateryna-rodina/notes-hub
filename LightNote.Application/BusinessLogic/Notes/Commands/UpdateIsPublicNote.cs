using System;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notes.Commands
{
	public class UpdateIsPublicNote : IRequest
	{
		public Guid Id { get; set; }
		public bool IsPublic { get; set; }
	}
}

