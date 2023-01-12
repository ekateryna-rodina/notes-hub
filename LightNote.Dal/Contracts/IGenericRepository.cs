using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Dal.Contracts
{
    public interface IGenericRepository<TEntity>
    {
        /// <summary>
        /// Returns entities
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>>? filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
            string includeProperties);
        /// <summary>
        /// Returns and entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByID(Guid id);
        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);
        /// <summary>
        /// Removes an entity
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid id);
        /// <summary>
        /// Uodates an entity
        /// </summary>
        /// <param name="entityToUpdate"></param>
        void Update(TEntity entityToUpdate);
    }
}

