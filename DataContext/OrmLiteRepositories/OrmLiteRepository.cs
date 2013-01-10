using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data;
using Application.DataInterface;
using Application.Models.Contract;
using ServiceStack.OrmLite;

namespace Application.DataContext.OrmLiteRepositories
{
    public class OrmLiteRepository<TEntity> : IRepository<TEntity>, IOrmLiteRepository where TEntity:class,new() 
    {
        public string ConnectionString { get; set; }

        /// <summary>
        /// Creates and if overriden where one can seed data
        /// </summary>
        public virtual void CreateMissingTables()
        {
            DbFactory.Run(db => db.CreateTable<TEntity>(overwrite:false));
        }

        protected IDbConnectionFactory DbFactory { get; set; }
        public OrmLiteRepository(IDbConnectionFactory dbFactory)
        {
            DbFactory = dbFactory;
        }  
        /// <summary>
        /// Add item to dbset
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(TEntity item)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                db.Insert(item);
                //OrmLite does not automagically update the item with the latest id inserted as Entity Framework does
                //Do this here, but make sure entity has an Id field
                if(item.GetType().GetMember("Id").Length > 0)
                    ((IEntity)item).Id = (int)db.GetLastInsertId();
            }          
        }

        public void AddAll(IEnumerable<TEntity> items)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                using(var dbTransaction = db.OpenTransaction())
                {
                    foreach (var item in items)
                    {
                        db.Insert(item);
                    }
                    dbTransaction.Commit();
                }                
            }
        }

        /// <summary>
        /// remove item from dbset
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(TEntity item)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                db.Delete(item);
            }          
        }

        public void RemoveAll(IEnumerable<TEntity> items)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                db.DeleteAll(items);
            }
        }

        public void RemoveAll(IEnumerable<long> ids)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                db.DeleteByIds<TEntity>(ids);
            }
        }

        /// <summary>
        /// Delete item with id 
        /// </summary>
        /// <param name="id">id of item to delete </param>
        public void Remove(int id)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                db.DeleteById<TEntity>(id);
            }                      
        }

        /// <summary>
        /// Retrieves an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Get(long id)
        {
            using (var db = DbFactory.OpenDbConnection())
            {                
               return db.GetById<TEntity>(id);
            }              
        }

        /// <summary>
        /// Updates the entity in the context
        /// </summary>
        /// <param name="item"></param>
        public void Update(TEntity item)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                db.Update(item);
            }                          
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves list of entities filtered by whereExpression
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> whereExpression)
        {
            using (IDbConnection db = DbFactory.OpenDbConnection())
            {
                return db.Select(whereExpression);
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
