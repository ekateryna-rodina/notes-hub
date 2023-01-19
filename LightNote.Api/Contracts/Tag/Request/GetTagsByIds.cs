using System;
using LightNote.Api.CustomValidators;

namespace LightNote.Api.Contracts.Tag.Request
{
	public class GetTagsByIds
	{
        public IEnumerable<Guid> TagIds { get; set; } = default!;
	}
}

