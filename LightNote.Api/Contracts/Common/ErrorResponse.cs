using System;
namespace LightNote.Api.Contracts.Common
{
    public class ErrorResponse
    {
        public ErrorResponse(int code, string phrase)
        {
            Errors = new();
            Code = code;
            Phrase = phrase;
            Timestamp = DateTimeOffset.UtcNow;
        }
        public int Code { get; set; }
        public string? Phrase { get; set; } = default!;
        public List<string> Errors { get; } = new();
        public DateTimeOffset Timestamp { get; set; }
    }
}

