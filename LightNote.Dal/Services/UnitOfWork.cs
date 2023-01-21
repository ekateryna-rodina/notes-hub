using System;
using LightNote.Dal.Contracts;
using LightNote.Dal.Repository;
using LightNote.Domain.Models.NotebookAggregate;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.UserProfileAggregate;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage;

namespace LightNote.Dal.Services
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly AppDbContext _context;
        private GenericRepository<UserProfile>? _userRepository;
        private GenericRepository<Notebook>? _notebookRepository;
        private GenericRepository<Tag>? _tagRepository;
        private GenericRepository<Reference>? _referenceRepository;
        private GenericRepository<PermanentNote>? _permanentNoteRepository;
        private GenericRepository<SlipNote>? _slipNoteRepository;
        private GenericRepository<Insight>? _insightRepository;
        private GenericRepository<Question>? _questionRepository;
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

        public GenericRepository<Notebook> NotebookRepository
        {
            get
            {

                if (_notebookRepository == null)
                {
                    _notebookRepository = new GenericRepository<Notebook>(_context);
                }
                return _notebookRepository;
            }
        }

        public GenericRepository<Reference> ReferenceRepository
        {
            get
            {
                if (_referenceRepository == null)
                {
                    _referenceRepository = new GenericRepository<Reference>(_context);
                }
                return _referenceRepository;
            }
        }

        public GenericRepository<Tag> TagRepository
        {
            get
            {
                if (_tagRepository == null)
                {
                    _tagRepository = new GenericRepository<Tag>(_context);
                }
                return _tagRepository;
            }
        }

        public GenericRepository<PermanentNote> PermanentNoteRepository
        {
            get
            {

                if (_permanentNoteRepository == null)
                {
                    _permanentNoteRepository = new GenericRepository<PermanentNote>(_context);
                }
                return _permanentNoteRepository;
            }
        }

        public GenericRepository<SlipNote> SlipNoteRepository
        {
            get
            {

                if (_slipNoteRepository == null)
                {
                    _slipNoteRepository = new GenericRepository<SlipNote>(_context);
                }
                return _slipNoteRepository;
            }
        }

        public GenericRepository<Insight> InsightRepository
        {
            get
            {

                if (_insightRepository == null)
                {
                    _insightRepository = new GenericRepository<Insight>(_context);
                }
                return _insightRepository;
            }
        }

        public GenericRepository<Question> QuestionRepository
        {
            get
            {

                if (_questionRepository == null)
                {
                    _questionRepository = new GenericRepository<Question>(_context);
                }
                return _questionRepository;
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

