using System;
namespace LightNote.Api.Contracts.Common
{
	public class ErrorResponse
	{
        public ErrorResponse()
        {
            Errors = new();
        }
        public int Code { get; set; }
        public string? Phrase { get; set; } = default!;
        public List<string> Errors { get; } = new();
        public DateTime Timestamp { get; set; }
    }
}

