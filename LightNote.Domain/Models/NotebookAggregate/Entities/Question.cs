using System;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.Common.ValueObjects;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using LightNote.Domain.Models.UserProfileAggregate;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;

namespace LightNote.Domain.Models.NotebookAggregate.Entities
{
    public class Question : Entity<QuestionId>
    {
        private Question(QuestionId id, Content content,
                InsightId insightId, UserProfileId userProfileId) : base(id)
        {
            Content = content;
            InsightId = insightId;
            UserProfileId = userProfileId;
        }
        public Content Content { get; private set; }
        public InsightId InsightId { get; private set; }
        public Insight Insight { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public static Question Create(string content, Guid insightId, Guid userId)
        {
            return new Question(QuestionId.Create(), Content.Create(content), InsightId.Create(insightId), UserProfileId.Create(userId));
        }
        public void Update(string content)
        {
            Content = Content.Create(content);
        }
    }
}
