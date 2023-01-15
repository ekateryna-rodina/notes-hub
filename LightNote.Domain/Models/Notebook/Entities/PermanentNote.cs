using System;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.Common.ValueObjects;
using LightNote.Domain.Models.Notebook.ValueObjects;
using LightNote.Domain.Models.UserProfile.ValueObjects;

namespace LightNote.Domain.Models.Notebook.Entities
{
    public class PermanentNote : Entity<PermanentNoteId>
    {
        private readonly List<SlipNote> _slipNotes = new();
        public PermanentNote(PermanentNoteId id, Title title,
            Content content, UserProfileId userProfileId) : base(id)
        {
            Title = title;
            Content = content;
            UserProfileId = userProfileId;
        }
        public Title Title { get; private set; }
        public Content Content { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public LightNote.Domain.Models.UserProfile.UserProfile UserProfile { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public IReadOnlyCollection<SlipNote> SlipNotes { get { return _slipNotes.AsReadOnly(); } }

        public static PermanentNote Create(string title, string content, Guid userProfileId)
        {
            return new(PermanentNoteId.Create(), Title.Create(title), Content.Create(content), UserProfileId.Create(userProfileId));
        }
        public void UpdateTitle(string title)
        {
            Title = Title.Create(title);
        }
        public void UpdateContent(string content)
        {
            Content = Content.Create(content);
        }
        public void AddSlipNote(SlipNote slipNote)
        {
            _slipNotes.Add(slipNote);
        }
        public void RemoveSlipNote(SlipNote slipNote)
        {
            _slipNotes.Remove(slipNote);
        }
        public void UpdateSlipNote(SlipNote oldSlipNote, SlipNote newSlipNote)
        {
            var index = _slipNotes.IndexOf(oldSlipNote);
            if (index != -1)
            {
                _slipNotes[index] = newSlipNote;
            }
        }
    }
}
