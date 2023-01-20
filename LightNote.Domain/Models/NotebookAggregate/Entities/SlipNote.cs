using System;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.Common.ValueObjects;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using LightNote.Domain.Models.UserProfileAggregate;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;

namespace LightNote.Domain.Models.NotebookAggregate.Entities
{
    public class SlipNote : Entity<SlipNoteId>
    {
        private SlipNote(SlipNoteId id, Content content, UserProfileId userProfileId,
                NotebookId notebookId, Reference reference) : base(id)
        {
            Content = content;
            UserProfileId = userProfileId;
            NotebookId = notebookId;
            Reference = reference;
        }
        private SlipNote() { }
        public Content Content { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public NotebookId NotebookId { get; private set; }
        public Notebook Notebook { get; private set; }
        public PermanentNoteId? PermanentNoteId { get; private set; }
        public PermanentNote? PermanentNote { get; private set; }
        public ReferenceId ReferenceId { get; private set; }
        public Reference Reference { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public static SlipNote Create(string content, Guid userProfileId, Guid notebookId, Reference reference)
        {
            return new(SlipNoteId.Create(), Content.Create(content),
                UserProfileId.Create(userProfileId), NotebookId.Create(notebookId), reference);
        }
        public void UpdateContent(string content)
        {
            Content = Content.Create(content);
        }
    }
}
