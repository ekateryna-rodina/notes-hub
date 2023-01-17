using System;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using LightNote.Domain.Models.UserProfileAggregate;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;

namespace LightNote.Domain.Models.NotebookAggregate.Entities
{
    public class Reference : Entity<ReferenceId>
    {
        private readonly List<ReferenceTag> _tagsAttached = new();
        private readonly List<QuestionReference> _relatedToQuestions = new();
        private readonly List<SlipNote> _slipNotes = new();
        private Reference(ReferenceId id, string name, bool isLink, UserProfileId userProfileId, NotebookId notebookId, IEnumerable<Tag> tags) : base(id)
        {
            Name = name;
            IsLink = isLink;
            UserProfileId = userProfileId;
            NotebookId = notebookId;
            SetTags(tags);
        }
        private Reference() { }
        public string Name { get; private set; }
        public bool IsLink { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public NotebookId NotebookId { get; private set; }
        public Notebook Notebook { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public IReadOnlyCollection<SlipNote> SlipNotes { get { return _slipNotes.AsReadOnly(); } }
        public IReadOnlyCollection<QuestionReference> RelatedQuestions { get { return _relatedToQuestions.AsReadOnly(); } }
        public IReadOnlyCollection<ReferenceTag> TagsAttached { get { return _tagsAttached.AsReadOnly(); } }
        public static Reference Create(string name, bool isLink, Guid userProfileId, Guid notebookId, IEnumerable<Tag> tags)
        {
            return new(ReferenceId.Create(), name, isLink, UserProfileId.Create(userProfileId),
                NotebookId.Create(notebookId), tags);
        }
        public void SetTags(IEnumerable<Tag> tags)
        {
            _tagsAttached.AddRange(tags.Select(t => new ReferenceTag {ReferenceId = Id, TagId = t.Id }));
        }
        public void SetQuestions(IEnumerable<Question> questions)
        {
            _relatedToQuestions
               .AddRange(questions
                   .Select(t => new QuestionReference { QuestionId = t.Id, ReferenceId = Id }));
        }
    }
}

