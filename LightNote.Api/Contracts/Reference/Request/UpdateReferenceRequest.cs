using System;
using System.ComponentModel.DataAnnotations;

namespace LightNote.Api.Contracts.Reference.Request
{
	public class UpdateReferenceRequest
	{
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public bool IsLink { get; set; } = default!;
        [Required]
        public Guid NotebookId { get; set; }
        [MinLength(1)]
        public IEnumerable<Guid> TagIds { get; set; } = default!;
    }
}
