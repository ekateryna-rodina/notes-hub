using LightNote.Dal.Repository;
using LightNote.Domain.Models.NotebookAggregate;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.UserProfileAggregate;
using Microsoft.EntityFrameworkCore.Storage;

namespace LightNote.Dal.Contracts
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Call to persist the entity in the database
        /// </summary>
        Task SaveAsync();
        /// <summary>
        /// Repository to manage user profiles
        /// </summary>
        GenericRepository<UserProfile> UserRepository { get; }
        /// <summary>
        /// Repository to manage notes
        /// </summary>
        GenericRepository<Notebook> NotebookRepository { get; }
        /// <summary>
        /// Repository to manage references
        /// </summary>
        GenericRepository<Reference> ReferenceRepository { get; }
        /// <summary>
        /// Repository to manage tags
        /// </summary>
        GenericRepository<Tag> TagRepository { get; }
        /// <summary>
        /// Repository to manage permanent notes
        /// </summary>
        GenericRepository<PermanentNote> PermanentNoteRepository { get; }
        /// <summary>
        /// Repository to manage slip notes
        /// </summary>
        GenericRepository<SlipNote> SlipNoteRepository { get; }
        /// <summary>
        /// Repository to manage insights
        /// </summary>
        GenericRepository<Insight> InsightRepository { get; }
        /// <summary>
        /// Repository to manage questions
        /// </summary>
        GenericRepository<Question> QuestionRepository { get; }
        /// <summary>
        /// Creates a transaction
        /// </summary>
        /// <returns></returns>
        IDbContextTransaction BeginTransaction();
    }
}

