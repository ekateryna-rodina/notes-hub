using System;
using LightNote.Dal.Repository;
using LightNote.Domain.Models.Note;
using LightNote.Domain.Models.User;
using Microsoft.EntityFrameworkCore.Storage;

namespace LightNote.Dal.Contracts
{
	public interface IUnitOfWork
	{
		/// <summary>
		/// Call to persist the entity in the database
		/// </summary>
		public void Save();
		/// <summary>
		/// Repository to manage user profiles
		/// </summary>
        GenericRepository<UserProfile> UserRepository { get; }
        /// <summary>
        /// Repository to manage notes
        /// </summary>
        GenericRepository<Note> NoteRepository { get; }
        /// <summary>
        /// Creates a transaction
        /// </summary>
        /// <returns></returns>
        IDbContextTransaction BeginTransaction();
    }
}

