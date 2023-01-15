using System;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;

namespace LightNote.Domain.Models.NotebookAggregate.Entities
{
    public sealed class Tag : Entity<TagId>
    {
        private Tag(TagId id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public static Tag Create(string name)
        {
            return new(TagId.Create(), name);
        }
    }
}

