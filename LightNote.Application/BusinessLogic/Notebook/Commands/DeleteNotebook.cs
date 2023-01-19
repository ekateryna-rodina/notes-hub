using System;
using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notebook.Commands
{
	public class DeleteNotebook : IRequest<OperationResult<bool>>
	{
		public Guid Id { get; set; }
		public Guid UserProfileId { get; set; }
	}
}

