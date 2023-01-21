using System.ComponentModel.DataAnnotations;

namespace LightNote.Api.Contracts.Question.Request
{
    public class UpdateQuestionRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; } = default!;
        [Required]
        public IEnumerable<Guid> ReferencesIds { get; set; } = default!;
    }
}