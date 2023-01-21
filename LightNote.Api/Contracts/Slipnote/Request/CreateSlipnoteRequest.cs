using System;
using System.ComponentModel.DataAnnotations;

namespace LightNote.Api.Contracts.SlipNote.Request
{
    public class CreateSlipNoteRequest
    {
        [Required]
        public string Content { get; set; } = default!;
        [Required]
        public Guid NotebookId { get; set; }
        [Required]
        public Guid ReferenceId { get; set; } = default!;
    }
}

