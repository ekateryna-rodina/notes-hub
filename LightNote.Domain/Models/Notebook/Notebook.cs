using System;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.Common.ValueObjects;
using LightNote.Domain.Models.Notebook.Entities;
using LightNote.Domain.Models.Notebook.ValueObjects;
using LightNote.Domain.Models.UserProfile.ValueObjects;

namespace LightNote.Domain.Models.Notebook
{
    public sealed class Notebook : Aggregate<NotebookId>
    {
        private readonly List<PermanentNote> _permanentNotes = new();
        private readonly List<Insight> _insights = new();
        private Notebook(NotebookId id, Title title, UserProfileId userProfileId) : base(id)
        {
            Title = title;
            UserProfileId = userProfileId;
        }
        public Title Title { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public LightNote.Domain.Models.UserProfile.UserProfile UserProfile { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public IReadOnlyCollection<PermanentNote> PermanentNotes { get { return _permanentNotes.AsReadOnly(); } }
        public IReadOnlyCollection<Insight> Insights { get { return _insights.AsReadOnly(); } }
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
        public void UpdatePermanentNote(PermanentNote oldPermanentNote, PermanentNote newPermanentNote)
        {
            var index = _permanentNotes.IndexOf(oldPermanentNote);
            if (index != -1)
            {
                _permanentNotes[index] = newPermanentNote;
            }
        }
        public void AddInsight(Insight insight)
        {
            _insights.Add(insight);
        }
        public void RemoveInsight(Insight insight)
        {
            _insights.Remove(insight);
        }
        public void UpdateInsight(Insight oldInsight, Insight newInsight)
        {
            var index = _insights.IndexOf(oldInsight);
            if (index != -1)
            {
                _insights[index] = newInsight;
            }
        }
    }
}
