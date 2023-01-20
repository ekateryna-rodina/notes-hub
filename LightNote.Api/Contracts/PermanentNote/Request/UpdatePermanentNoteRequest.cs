namespace LightNote.Api.Contracts.PermanentNote.Request
{
    public class UpdatePermanentNoteRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public IEnumerable<Guid> SlipNoteIds { get; set; } = default!;
    }
}