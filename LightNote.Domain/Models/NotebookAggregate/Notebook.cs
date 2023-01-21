using System;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.Common.ValueObjects;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using LightNote.Domain.Models.UserProfileAggregate;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;

namespace LightNote.Domain.Models.NotebookAggregate
{
    public sealed class Notebook : Aggregate<NotebookId>
    {
        private readonly List<PermanentNote> _permanentNotes = new();
        private readonly List<Insight> _insights = new();
        private readonly List<Reference> _references = new();
        private readonly List<SlipNote> _slipNotes = new();
        private readonly List<Question> _questions = new();
        private Notebook(NotebookId id, Title title, UserProfileId userProfileId) : base(id)
        {
            Title = title;
            UserProfileId = userProfileId;
        }
        private Notebook() { }
        public Title Title { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public IReadOnlyCollection<PermanentNote> PermanentNotes { get { return _permanentNotes.AsReadOnly(); } }
        public IReadOnlyCollection<Insight> Insights { get { return _insights.AsReadOnly(); } }
        public IReadOnlyCollection<Reference> References { get { return _references.AsReadOnly(); } }
        public IReadOnlyCollection<SlipNote> SlipNotes { get { return _slipNotes.AsReadOnly(); } }
        public IReadOnlyCollection<Question> Questions { get { return _questions.AsReadOnly(); } }
        public static Notebook Create(string title, Guid userProfileId)
        {
            return new(NotebookId.Create(),
                                Title.Create(title),
                                UserProfileId.Create(userProfileId));
        }
        public void UpdateTitle(string title)
        {
            Title = Title.Create(title);
        }
        public void AddPermanentNote(PermanentNote permanentNote)
        {
            _permanentNotes.Add(permanentNote);
        }
        public void RemovePermanentNote(PermanentNote permanentNote)
        {
            _permanentNotes.Remove(permanentNote);
        }
        public void AddInsight(Insight insight)
        {
            _insights.Add(insight);
        }
        public void RemoveInsight(Insight insight)
        {
            _insights.Remove(insight);
        }
        public void AddReference(Reference reference)
        {
            _references.Add(reference);
        }
        public void RemoveReference(Reference reference)
        {
            _references.Remove(reference);
        }
        public void AddSlipNote(SlipNote slipNote)
        {
            _slipNotes.Add(slipNote);
        }
        public void RemoveSlipNote(SlipNote slipNote)
        {
            _slipNotes.Remove(slipNote);
        }
        public void AddQuestion(Question question)
        {
            _questions.Add(question);
        }
        public void RemoveQuestion(Question question)
        {
            _questions.Remove(question);
        }
    }
}
