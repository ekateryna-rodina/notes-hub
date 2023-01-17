using System;
using LightNote.Domain.Models.NotebookAggregate;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;

namespace LightNote.Domain.Models.UserProfileAggregate
{
    public class UserProfile
    {
        private List<Notebook> _notebooks = new List<Notebook>();
        private List<Reference> _references = new List<Reference>();
        private List<SlipNote> _slipNotes = new List<SlipNote>();
        private List<PermanentNote> _permanentNotes = new List<PermanentNote>();
        private List<Insight> _insights = new List<Insight>();
        private List<Question> _questions = new List<Question>();
        private UserProfile()
        {

        }
        public UserProfileId Id { get; private set; }
        public string IdentityId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhotoUrl { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public IReadOnlyCollection<Reference> References { get { return _references.AsReadOnly(); } }
        public IReadOnlyCollection<SlipNote> SlipNotes { get { return _slipNotes.AsReadOnly(); } }
        public IReadOnlyCollection<PermanentNote> PermanentNotes { get { return _permanentNotes.AsReadOnly(); } }
        public IReadOnlyCollection<Insight> Insights { get { return _insights.AsReadOnly(); } }
        public IReadOnlyCollection<Question> Questions { get { return _questions.AsReadOnly(); } }
        public IReadOnlyCollection<Notebook> Notebooks { get { return _notebooks.AsReadOnly(); } }

        public static UserProfile CreateUserProfile(string identityId,
            string firstName, string lastName, string photoUrl, string country, string city)
        {
            return new UserProfile
            {
                Id = UserProfileId.Create(),
                FirstName = firstName,
                LastName = lastName,
                PhotoUrl = photoUrl,
                Country = country,
                City = city,
                IdentityId = identityId,
                CreatedAt = DateTime.UtcNow
            };
        }
        public void UpdateFirstName(string firstName)
        {
            FirstName = firstName;
        }
        public void UpdateLastName(string lastName)
        {
            LastName = lastName;
        }
        public void UpdatePhotoUrl(string photoUrl)
        {
            PhotoUrl = photoUrl;
        }
        public void Updatelocation(string country, string city)
        {
            Country = country;
            City = city;
        }
    }
}

