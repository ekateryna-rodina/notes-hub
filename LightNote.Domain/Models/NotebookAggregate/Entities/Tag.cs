using System;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using LightNote.Domain.Models.UserProfileAggregate;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;

namespace LightNote.Domain.Models.NotebookAggregate.Entities
{
    public class Tag
    {
        public TagId Id { get; private set; }
        public string Name { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public UserProfile UserProfiled { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
    }
}

