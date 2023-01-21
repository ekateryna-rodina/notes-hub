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
        private readonly List<InsightPermanentNote> _basedOnPeranentNotes = new();
        private Insight(InsightId id, Title title, Content content, UserProfileId userProfileId, NotebookId notebookId, IEnumerable<PermanentNote> permanentNotes) : base(id)
        {
            Title = title;
            Content = content;
            UserProfileId = userProfileId;
            NotebookId = notebookId;
            SetPermanentNotes(permanentNotes);
        }
        private Insight()
        {
        }
        public Title Title { get; private set; }
        public Content Content { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public NotebookId NotebookId { get; private set; }
        public Notebook Notebook { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public IReadOnlyCollection<InsightPermanentNote> BasedOnPermanentNotes { get { return _basedOnPeranentNotes.AsReadOnly(); } }
        public IReadOnlyCollection<Question> Questions { get { return _questions.AsReadOnly(); } }
        public static Insight Create(string title, string content, Guid userProfileId, Guid notebookId, IEnumerable<PermanentNote> permanentNotes)
        {
            return new(InsightId.Create(), Title.Create(title), Content.Create(content), UserProfileId.Create(userProfileId), NotebookId.Create(notebookId), permanentNotes);
        }
        public void UpdateContent(string content)
        {
            Content = Content.Create(content);
        }
        public void UpdateTitle(string content)
        {
            Title = Title.Create(content);
        }
        public void SetPermanentNotes(IEnumerable<PermanentNote> permanentNotes)
        {
            _basedOnPeranentNotes
                .AddRange(permanentNotes
                    .Select(pn => new InsightPermanentNote { InsightId = Id, PermanentNoteId = pn.Id }));
        }
    }
}




