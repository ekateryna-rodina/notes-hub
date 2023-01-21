using System;
namespace LightNote.Api.Contracts.Notebook.Response
{
	public class NotebookResponse
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = default!;
		public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}

