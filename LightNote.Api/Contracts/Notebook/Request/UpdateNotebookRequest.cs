using System;
namespace LightNote.Api.Contracts.Notebook.Request
{
	public class UpdateNotebookRequest
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = default!;
	}
}

