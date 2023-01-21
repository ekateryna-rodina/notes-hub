using System;
using LightNote.Application.Helpers;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using MediatR;

namespace LightNote.Application.BusinessLogic.Tags.Queries
{
	public class GetTagsByIds : IRequest<OperationResult<IEnumerable<Tag>>>
    {
		public IEnumerable<Guid> TagIds { get; set; } = default!;
	}
}

