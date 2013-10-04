using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Application.DataInterface;
using Application.Models.Contract;

namespace Application.BusinessLogic.Contracts
{
    public interface IService<TRepository, TEntity> : IDisposable
        where TRepository : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Add the entity to the database.
        /// </summary>
        /// <param name="item"></param>
        void Add(TEntity item);

        /// <summary>
        /// Add list of entities to the database
        /// </summary>
        /// <param name="items"></param>
        void AddAll(IEnumerable<TEntity> items);

        /// <summary>
        /// Gets the entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(long id);

        /// <summary>
        /// Gets the entity from the database
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(IEnumerable<long> ids);

        /// <summary>
        /// Gets the entity from the database
        /// </summary>        
        /// <param name="whereExpression">Where expression</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// Updates the entity from the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        void Update(TEntity item);

        /// <summary>
        /// Deletes the entity from the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        void Delete(TEntity item);

        /// <summary>
        /// Deletes the entity by Id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Delete(long id);

        /// <summary>
        /// Deletes a list of entites from the database
        /// </summary>
        /// <param name="items"></param>
        void DeleteAll(IEnumerable<TEntity> items);

        /// <summary>
        /// Deletes a list of entites by id from the database
        /// </summary>
        /// <param name="ids"></param>
        void DeleteAll(IEnumerable<long> ids);

    }
}
