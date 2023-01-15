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
        private readonly List<Reference> _references = new();
        private SlipNote(SlipNoteId id, Content content, UserProfileId userProfileId,
                NotebookId notebookId, IEnumerable<Reference> references) : base(id)
        {
            Content = content;
            UserProfileId = userProfileId;
            NotebookId = notebookId;
            SetReferences(references);
        }

        public Content Content { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public NotebookId NotebookId { get; private set; }
        public Notebook Notebook { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public IReadOnlyCollection<Reference> References { get { return _references.AsReadOnly(); } }
        public static SlipNote Create(string content, Guid userProfileId, Guid notebookId, IEnumerable<Reference> references)
        {
            return new(SlipNoteId.Create(), Content.Create(content), UserProfileId.Create(userProfileId), NotebookId.Create(notebookId), references);
        }
        public void SetReferences(IEnumerable<Reference> references)
        {
            _references.Clear();
            _references.AddRange(references);
        }
        public void UpdateContent(string content)
        {
            Content = Content.Create(content);
        }
    }
}
