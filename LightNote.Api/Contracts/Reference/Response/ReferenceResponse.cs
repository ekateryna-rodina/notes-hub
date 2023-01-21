namespace LightNote.Api.Contracts.Reference.Response
{
    public class ReferenceResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsLink { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Dictionary<Guid, string> Tags { get; set; } = default!;
    }
}

