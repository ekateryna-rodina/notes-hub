using System;
using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.References.Commands
{
	public class DeleteReference : IRequest<OperationResult<bool>>
	{
		public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
    }
}

