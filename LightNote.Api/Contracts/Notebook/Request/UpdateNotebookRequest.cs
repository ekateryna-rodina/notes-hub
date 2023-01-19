using System;
using System.ComponentModel.DataAnnotations;

namespace LightNote.Api.Contracts.Notebook.Request
{
    public class UpdateNotebookRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; } = default!;
    }
}

