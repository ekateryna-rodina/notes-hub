using System;
using LightNote.Dal.Contracts;
using LightNote.Dal.Repository;
using LightNote.Domain.Models.Note;
using LightNote.Domain.Models.User;
using Microsoft.EntityFrameworkCore.Storage;

namespace LightNote.Dal.Services
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly AppDbContext _context;
        private GenericRepository<UserProfile>? _userRepository;
        private GenericRepository<Note>? _noteRepository;
        private IDbContextTransaction? _transaction;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public GenericRepository<UserProfile> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<UserProfile>(_context);
                }
                return _userRepository;
            }
        }

        public GenericRepository<Note> NoteRepository
        {
            get
            {

                if (_noteRepository == null)
                {
                    _noteRepository = new GenericRepository<Note>(_context);
                }
                return _noteRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}

