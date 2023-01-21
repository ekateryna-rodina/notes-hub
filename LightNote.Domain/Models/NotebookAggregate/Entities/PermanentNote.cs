using System;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.Common.ValueObjects;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using LightNote.Domain.Models.UserProfileAggregate;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;

namespace LightNote.Domain.Models.NotebookAggregate.Entities
{
    public class PermanentNote : Entity<PermanentNoteId>
    {
        private readonly List<SlipNote> _slipNotes = new();
        private readonly List<InsightPermanentNote> _createdInsights = new();
        private readonly List<Question> _questions = new();
        private PermanentNote(PermanentNoteId id, Title title,
            Content content, UserProfileId userProfileId, NotebookId notebookId, IEnumerable<SlipNote> slipNotes) : base(id)
        {
            Title = title;
            Content = content;
            UserProfileId = userProfileId;
            NotebookId = notebookId;
            SetSlipNotes(slipNotes);
        }
        private PermanentNote() { }
        public Title Title { get; private set; }
        public Content Content { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public NotebookId NotebookId { get; private set; }
        public Notebook Notebook { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public IReadOnlyCollection<SlipNote> SlipNotes { get { return _slipNotes.AsReadOnly(); } }
        public IReadOnlyCollection<InsightPermanentNote> Insights { get { return _createdInsights.AsReadOnly(); } }
        public IReadOnlyCollection<Question> Questions { get { return _questions.AsReadOnly(); } }

        public static PermanentNote Create(string title, string content, Guid userProfileId, Guid notebookId, IEnumerable<SlipNote> slipNotes)
        {
            return new(PermanentNoteId.Create(), Title.Create(title), Content.Create(content),
            UserProfileId.Create(userProfileId), NotebookId.Create(notebookId), slipNotes);
        }
        public void UpdateTitle(string title)
        {
            Title = Title.Create(title);
        }
        public void UpdateContent(string content)
        {
            Content = Content.Create(content);
        }
        public void SetSlipNotes(IEnumerable<SlipNote> slipNotes)
        {
            _slipNotes.Clear();
            _slipNotes.AddRange(slipNotes);
        }
    }
}
