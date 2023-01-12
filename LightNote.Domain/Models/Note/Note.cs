using System;
using LightNote.Domain.Models.User;

namespace LightNote.Domain.Models.Note
{
    public class Note
    {
        private readonly List<Tag> _tags = new List<Tag>();
        private readonly List<Note> _links = new List<Note>();

        private Note()
        {

        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public bool IsPublic { get; private set; }
        public Guid ReferenceId { get; private set; }
        public Reference Reference { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public virtual IEnumerable<Tag> Tags { get { return _tags; } }
        public virtual IEnumerable<Note> Links { get { return _links; } }

        public static Note CreateNote(Guid userId, string title, string content, bool isPublic)
        {
            return new Note
            {
                UserId = userId,
                Title = title,
                Content = content,
                IsPublic = isPublic,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void UpdateTitle(string title)
        {
            Title = title;
            UpdatedAt = DateTime.UtcNow;
        }
        public void UpdateContent(string content)
        {
            Content = content;
            UpdatedAt = DateTime.UtcNow;
        }
        public void UpdateIsPublic(bool isPublic)
        {
            IsPublic = isPublic;
            UpdatedAt = DateTime.UtcNow;
        }
        public void AddTag(Tag tag)
        {
            _tags.Add(tag);
            UpdatedAt = DateTime.UtcNow;
        }
        public void RemoveTag(Tag tag)
        {
            _tags.Remove(tag);
            UpdatedAt = DateTime.UtcNow;
        }
        public void UpdateReference(Guid referenceId)
        {
            ReferenceId = referenceId;
        }
        public void AddLink(Note note)
        {
            _links.Add(note);
        }
        public void RemoveLink(Note note)
        {
            _links.Remove(note);
        }
    }
}

