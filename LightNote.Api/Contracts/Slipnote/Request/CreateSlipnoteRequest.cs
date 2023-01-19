using System;
using System.ComponentModel.DataAnnotations;

namespace LightNote.Api.Contracts.Slipnote.Request
{
	public class CreateSlipnoteRequest
	{
        [Required]
        public string Content { get; set; } = default!;
        [Required]
        public Guid NotebookId { get; set; }
        [Required]
        public Guid ReferenceId { get; set; } = default!;
    }
}

