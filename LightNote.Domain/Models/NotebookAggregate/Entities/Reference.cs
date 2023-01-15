using System;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using LightNote.Domain.Models.UserProfileAggregate;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;

namespace LightNote.Domain.Models.NotebookAggregate.Entities
{
    public class Reference : Entity<ReferenceId>
    {
        private readonly List<Tag> _tags = new();
        private readonly List<Question> _questions = new();
        private Reference(ReferenceId id, string name, bool isLink, UserProfileId userProfileId, NotebookId notebookId, IEnumerable<Tag> tags) : base(id)
        {
            Name = name;
            IsLink = isLink;
            UserProfileId = userProfileId;
            NotebookId = notebookId;

            SetTags(tags);
        }
        public string Name { get; private set; }
        public bool IsLink { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public NotebookId NotebookId { get; private set; }
        public Notebook Notebook { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public static Reference Create(string name, bool isLink, Guid userProfileId, Guid notebookId, IEnumerable<Tag> tags)
        {
            return new(ReferenceId.Create(), name, isLink, UserProfileId.Create(userProfileId),
                NotebookId.Create(notebookId), tags);
        }
        public void SetTags(IEnumerable<Tag> tags)
        {
            _tags.Clear();
            _tags.AddRange(tags);
        }
        public void RemoveTag(Tag tag)
        {
            _tags.Remove(tag);
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

