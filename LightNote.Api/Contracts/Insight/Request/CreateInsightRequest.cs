using System.ComponentModel.DataAnnotations;

namespace LightNote.Api.Contracts.Insight.Request
{
    public class CreateInsightRequest
    {
        [Required]
        public string Title { get; set; } = default!;
        [Required]
        public string Content { get; set; } = default!;
        [Required]
        public Guid NotebookId { get; private set; }
        [Required]
        [MinLength(2, ErrorMessage = "Please choose at least 3 permanent notes to create an insight")]
        public IEnumerable<Guid> PermanentNoteIds { get; set; } = default!;
        public IEnumerable<string> Questions { get; set; } = default!;
    }
}