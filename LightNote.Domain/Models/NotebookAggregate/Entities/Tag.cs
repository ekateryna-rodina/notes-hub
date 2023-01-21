using System;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;

namespace LightNote.Domain.Models.NotebookAggregate.Entities
{
    public sealed class Tag : Entity<TagId>
    {
        private List<ReferenceTag> _referencesAttached = new List<ReferenceTag>();
        private Tag(TagId id, string name) : base(id)
        {
            Name = name;
        }
        private Tag() { }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public IReadOnlyCollection<ReferenceTag> ReferencesAttached { get { return _referencesAttached; } }
        public static Tag Create(string name)
        {
            return new(TagId.Create(), name);
        }
    }
}

