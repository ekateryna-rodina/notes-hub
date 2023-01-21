namespace LightNote.Api.Contracts.Question.Response
{
    public class QuestionResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public Guid? InsightId { get; set; } = default!;
        public string? InsightTitle { get; set; } = default!;
        public Guid PermanentNoteId { get; set; } = default!;
        public string PermanentNoteTitle { get; set; } = default!;
        public Guid NotebookId { get; set; } = default!;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public IDictionary<Guid, string> References { get; set; } = default!;
    }
}