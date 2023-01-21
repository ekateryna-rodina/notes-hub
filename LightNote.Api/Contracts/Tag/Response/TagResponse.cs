using System;
namespace LightNote.Api.Contracts.Tag.Response
{
	public class TagResponse
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = default!;
	}
}

