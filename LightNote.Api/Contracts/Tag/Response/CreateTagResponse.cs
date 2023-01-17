using System;
namespace LightNote.Api.Contracts.Tag.Response
{
	public class CreateTagResponse
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = default!;
	}
}

