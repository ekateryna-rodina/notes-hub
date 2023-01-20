using System;
using System.ComponentModel.DataAnnotations;

namespace LightNote.Api.Contracts.SlipNote.Request
{
    public class UpdateSlipNoteRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; } = default!;
        [Required]
        public Guid NotebookId { get; set; }
        [Required]
        public Guid ReferenceId { get; set; } = default!;
    }
}

