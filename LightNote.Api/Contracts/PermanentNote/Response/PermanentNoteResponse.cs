using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightNote.Api.Contracts.PermanentNote.Response
{
    public class PermanentNoteResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public Guid NotebookId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Dictionary<Guid, string> SlipNotes { get; set; } = default!;
    }
}