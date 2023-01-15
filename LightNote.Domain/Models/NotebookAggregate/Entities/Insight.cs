using System;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.Common.ValueObjects;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using LightNote.Domain.Models.UserProfileAggregate;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;

namespace LightNote.Domain.Models.NotebookAggregate.Entities
{
    public class Insight : Entity<InsightId>
    {
        private readonly List<Question> _questions = new();
        private readonly List<PermanentNote> permanentNotes = new();

        public Insight(InsightId id, Content content, UserProfileId userProfileId) : base(id)
        {
            Content = content;
            UserProfileId = userProfileId;
        }

        public Content Content { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public static Insight Create(string content, Guid userProfileId)
        {
            return new(InsightId.Create(), Content.Create(content), UserProfileId.Create(userProfileId));
        }
    }
}




