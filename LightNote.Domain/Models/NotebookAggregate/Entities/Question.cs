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
                NotebookId notebookId, UserProfileId userProfileId,
                InsightId insightId = null, PermanentNoteId permanentNoteId = null) : base(id)
        {
            Content = content;
            InsightId = insightId;
            PermanentNoteId = permanentNoteId;
            NotebookId = notebookId;
            UserProfileId = userProfileId;
        }
        public Content Content { get; private set; }
        public InsightId InsightId { get; private set; } = default!;
        public Insight Insight { get; private set; } = default!;
        public PermanentNoteId PermanentNoteId { get; private set; } = default!;
        public PermanentNote PermanentNote { get; private set; } = default!;
        public NotebookId NotebookId { get; private set; } = default!;
        public Notebook Notebook { get; private set; } = default!;
        public UserProfileId UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public static Question Create(string content, Guid notebookId, Guid userId, Guid? insightId = null, Guid? permanentNoteId = null)
        {
            return new Question(QuestionId.Create(), Content.Create(content), NotebookId.Create(notebookId),
            UserProfileId.Create(userId), insightId == null ? null : InsightId.Create(insightId),
            permanentNoteId == null ? null : PermanentNoteId.Create(permanentNoteId));
        }
        public void UpdateContent(string content)
        {
            Content = Content.Create(content);
        }
    }
}
