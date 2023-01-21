using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Build.Framework;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace LightNote.Api.Contracts.Reference.Request
{
    public class CreateReferenceRequest
    {
        [Required]
        public string Name { get; set; } = default!;
        public bool IsLink { get; set; } = default!;
        [Required]
        public Guid NotebookId { get; set; }
        [MinLength(1)]
        public IEnumerable<Guid> TagIds { get; set; } = default!;
    }
}
