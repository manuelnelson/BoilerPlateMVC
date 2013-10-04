using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Application.DataInterface;
using Application.Models.Contract;

namespace Application.DataContext.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public string ConnectionString
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        protected DataContext DataContext
        {
            get { return (DataContext)UnitOfWork; }
        }

        public Repository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// returns the dbset
        /// </summary>
        /// <returns></returns>
        public virtual DbSet<TEntity> GetDbSet()
        {
            return DataContext.Set<TEntity>();
        }

        /// <summary>
        /// sets the state for an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityState"></param>
        protected virtual void SetEntityState(TEntity entity, EntityState entityState)
        {
            //DataContext.Entry(entity).State = entityState;
            var v = GetDbSet().Find(entity.Id);// model.ReportDate
            DataContext.Entry(v).CurrentValues.SetValues(entity);
        }

        /// <summary>
        /// Add item to dbset
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(TEntity item)
        {
            GetDbSet().Add(item);
            UnitOfWork.Commit();
        }

        public void AddAll(IEnumerable<TEntity> items)
        {
            var dbSet = GetDbSet();
            foreach (var entity in items)
            {
                dbSet.Add(entity);
            }
            UnitOfWork.Commit();
        }

        /// <summary>
        /// remove item from dbset
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(TEntity item)
        {
            //attach item if it does not exist
            GetDbSet().Attach(item);

            //set as "removed"
            GetDbSet().Remove(item);
            UnitOfWork.Commit();
        }

        public void Remove(long id)
        {
            var item = GetDbSet().Find(id);
            //set as "removed"
            GetDbSet().Remove(item);
            UnitOfWork.Commit();
        }

        public void RemoveAll(IEnumerable<TEntity> items)
        {
            var dbSet = GetDbSet();
            foreach (var entity in items)
            {
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
            UnitOfWork.Commit();
        }

        public void RemoveAll(IEnumerable<long> ids)
        {
            var dbSet = GetDbSet();
            foreach (var entity in ids.Select(id => dbSet.Find(id)))
            {
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
            UnitOfWork.Commit();
        }

        /// <summary>
        /// Retrieves an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Get(long id)
        {
            return GetDbSet().Find(id);
        }


        public IEnumerable<TEntity> Get(IEnumerable<long> ids)
        {
            return GetDbSet().Where(x => ids.Contains(x.Id));
        }


        /// <summary>
        /// Updates the entity in the context
        /// </summary>
        /// <param name="item"></param>
        public void Update(TEntity item)
        {
            GetDbSet().Attach(item);
            SetEntityState(item, EntityState.Modified);
            UnitOfWork.Commit();
        }


        /// <summary>
        /// Retrieves a paged list
        /// </summary>
        /// <typeparam name="TKProperty"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetPaged<TKProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, TKProperty>> orderByExpression, bool ascending)
        {
            var set = GetDbSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            return set.OrderByDescending(orderByExpression)
                .Skip(pageCount * pageIndex)
                .Take(pageCount);
        }

        /// <summary>
        /// Retrieves list of entities filtered by whereExpression
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> whereExpression)
        {
            return GetDbSet().Where(whereExpression);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (UnitOfWork != null)
                UnitOfWork.Dispose();
        }
    }
}
