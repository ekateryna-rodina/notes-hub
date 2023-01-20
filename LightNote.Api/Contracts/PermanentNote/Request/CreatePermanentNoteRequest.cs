using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LightNote.Api.Contracts.PermanentNote.Request
{
    public class CreatePermanentNoteRequest
    {
        [Required]
        public string Title { get; private set; } = default!;
        [Required]
        public string Content { get; private set; } = default!;
        [Required]
        public Guid NotebookId { get; private set; }
        [Required]
        [MinLength(3, ErrorMessage = "Please choose at least 3 slip notes to create a permanent note")]
        public IEnumerable<Guid> SlipNoteIds { get; set; } = default!;

    }
}

