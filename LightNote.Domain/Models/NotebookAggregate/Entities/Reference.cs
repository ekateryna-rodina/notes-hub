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
        public Reference(ReferenceId id, string name, bool isLink, UserProfileId userProfileId) : base(id)
        {
            Name = name;
            IsLink = isLink;
            UserProfileId = userProfileId;
        }
        public string Name { get; private set; }
        public bool IsLink { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public static Reference Create(string name, bool isLink, Guid userProfileId)
        {
            return new(ReferenceId.Create(), name, isLink, UserProfileId.Create(userProfileId));
        }
        public void AddTag(Tag tag)
        {
            _tags.Add(tag);
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

