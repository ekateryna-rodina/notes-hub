using System;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.Common.ValueObjects;
using LightNote.Domain.Models.Notebook.ValueObjects;
using LightNote.Domain.Models.UserProfile.ValueObjects;

namespace LightNote.Domain.Models.Notebook.Entities
{
    public class SlipNote : Entity<SlipNoteId>
    {
        private readonly List<Reference> _references = new();

        private SlipNote(SlipNoteId id) : base(id)
        {
        }

        public Content Content { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public LightNote.Domain.Models.UserProfile.UserProfile UserProfile { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
    }
}
