using System;
using System.ComponentModel.DataAnnotations;

namespace LightNote.Api.Contracts.Notebook.Request
{
	public class CreateNotebookRequest
	{
		[Required]
		public string Title { get; set; } = default!;
	}
}

