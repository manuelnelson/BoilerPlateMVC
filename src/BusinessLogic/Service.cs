using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Application.BusinessLogic.Contracts;
using Application.DataInterface;
using Application.Models.Contract;
using Elmah;

namespace Application.BusinessLogic
{
    public class Service<TRepository, TEntity> : IService<TRepository, TEntity>
        where TRepository : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public TRepository Repository { get; set; }

        public Service(TRepository repository)
        {
            Repository = repository;
        }

        public void Add(TEntity item)
        {
            try
            {
                if (item != null) Repository.Add(item);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to add item", ex);
            }
        }

        public void AddAll(IEnumerable<TEntity> items)
        {
            try
            {
                Repository.AddAll(items);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to add item", ex);
            }
        }

        public TEntity Get(long id)
        {
            try
            {
                return Repository.Get(id);                
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve item", ex);
            }
        }

        public IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                return Repository.GetFiltered(whereExpression);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve item", ex);
            }
        }

        public void Update(TEntity item)
        {
            try
            {
                Repository.Update(item);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to update information", ex);
            }
        }

        public void Delete(TEntity item)
        {
            try
            {
                Repository.Remove(item);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to remove item", ex);
            }
        }

        public void DeleteAll(IEnumerable<TEntity> items)
        {
            try
            {
                Repository.RemoveAll(items);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to remove items", ex);
            }
        }

        public void DeleteAll(IEnumerable<long> ids)
        {
            try
            {
                Repository.RemoveAll(ids);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to remove items", ex);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {

        }
    }
}
