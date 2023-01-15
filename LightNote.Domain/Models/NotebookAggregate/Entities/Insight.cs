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
        private readonly List<PermanentNote> _permanentNotes = new();

        public Insight(InsightId id, Content content, UserProfileId userProfileId, NotebookId notebookId, IEnumerable<PermanentNote> permanentNotes) : base(id)
        {
            Content = content;
            UserProfileId = userProfileId;
            NotebookId = notebookId;
            SetPermanentNotes(permanentNotes);
        }

        public Content Content { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public NotebookId NotebookId { get; private set; }
        public Notebook Notebook { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public IReadOnlyCollection<PermanentNote> PermanentNotes { get { return _permanentNotes.AsReadOnly(); } }
        public static Insight Create(string content, Guid userProfileId, Guid notebookId, IEnumerable<PermanentNote> permanentNotes)
        {
            return new(InsightId.Create(), Content.Create(content), UserProfileId.Create(userProfileId), NotebookId.Create(notebookId), permanentNotes);
        }
        public void UpdateContent(string content)
        {
            Content = Content.Create(content);
        }
        public void SetPermanentNotes(IEnumerable<PermanentNote> permanentNotes)
        {
            _permanentNotes.Clear();
            _permanentNotes.AddRange(permanentNotes);
        }
    }
}




