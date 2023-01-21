using System.ComponentModel.DataAnnotations;

namespace LightNote.Api.Contracts.Insight.Request
{
    public class UpdateInsightRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = default!;
        [Required]
        public string Content { get; set; } = default!;
        [Required]
        [MinLength(2, ErrorMessage = "There should be at least 2 pemanent notes associated with an insight")]
        public IEnumerable<Guid> PermanentNoteIds { get; set; } = default!;
        public IEnumerable<string> Questions { get; set; } = default!;
    }
}