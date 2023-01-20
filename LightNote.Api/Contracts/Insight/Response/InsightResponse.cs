namespace LightNote.Api.Contracts.Insight.Response
{
    public class InsightResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public Guid NotebookId { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public IDictionary<Guid, string> BasedOnPermanentNotes { get; set; } = default!;
        public IDictionary<Guid, string> Questions { get; set; } = default!;
    }
}