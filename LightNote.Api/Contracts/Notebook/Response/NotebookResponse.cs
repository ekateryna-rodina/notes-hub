using System;
namespace LightNote.Api.Contracts.Notebook.Response
{
	public class NotebookResponse
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = default!;
		public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

