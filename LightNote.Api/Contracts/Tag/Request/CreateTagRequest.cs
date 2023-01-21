using System;
using System.ComponentModel.DataAnnotations;
using LightNote.Api.CustomValidators;

namespace LightNote.Api.Contracts.Tag.Request
{
	public class CreateTagRequest
	{
        [Required]
		[MinLengthAttribute(1)]
		[NonEmptyStringCollection]
		public IEnumerable<string> Names { get; set; } = default!;
	}
}

