using System;
namespace LightNote.Api.Contracts.SlipNote.Response
{
    public class SlipNoteResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public Guid NotebookId { get; set; }
        public Guid? PermanentNoteId { get; set; }
        public Guid ReferenceId { get; set; }
        public string ReferenceName { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

