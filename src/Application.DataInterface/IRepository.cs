using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Application.DataInterface
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        /// <summary>
        /// Add item into repository
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        void Add(TEntity item);

        /// <summary>
        /// Adds a list of items to a repository
        /// </summary>
        /// <param name="items"></param>
        void AddAll(IEnumerable<TEntity> items);

        /// <summary>
        /// Delete item 
        /// </summary>
        /// <param name="item">Item to delete</param>
        void Remove(TEntity item);

        /// <summary>
        /// Delete all items
        /// </summary>
        /// <param name="items"></param>
        void RemoveAll(IEnumerable<TEntity> items);

        /// <summary>
        /// Delete all items by Id 
        /// </summary>
        /// <param name="ids"></param>
        void RemoveAll(IEnumerable<long> ids);

        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="id">Entity key value</param>
        /// <returns></returns>
        TEntity Get(long id);

        /// <summary>
        /// Updates the entity in the context
        /// </summary>
        /// <param name="item"></param>
        void Update(TEntity item);

        /// <summary>
        /// Get all elements of type TEntity in repository
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="orderByExpression">Order by expression for this query</param>
        /// <param name="ascending">Specify if order is ascending</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetPaged<TKProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, TKProperty>> orderByExpression, bool ascending);

        /// <summary>
        /// Get  elements of type TEntity in repository
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter);
    }

}
