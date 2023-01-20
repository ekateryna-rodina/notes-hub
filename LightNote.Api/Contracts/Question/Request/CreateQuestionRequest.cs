using System.ComponentModel.DataAnnotations;

namespace LightNote.Api.Contracts.Question.Request
{
    public class CreateQuestionRequest
    {
        [Required]
        public string Content { get; set; } = default!;
        public Guid? InsightId { get; set; } = default!;
        public Guid PermanentNoteId { get; set; } = default!;
        [Required]
        public Guid NotebookId { get; set; } = default!;
    }
}