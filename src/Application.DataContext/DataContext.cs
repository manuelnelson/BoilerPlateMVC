using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Application.DataInterface;
using Application.Models;

namespace Application.DataContext
{
    public class DataContext : DbContext, IUnitOfWork
    {
        public DbSet<ToDo> Todos { get; set; }
        public DbSet<Test> Tests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetupTodoMapping(modelBuilder);
            SetupTestMapping(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SetupTestMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Test>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupTodoMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().HasKey(t => new { t.Id});
            modelBuilder.Entity<ToDo>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }

        void IUnitOfWork.Commit()
        {
            base.SaveChanges();
        }


        /// <summary>
        /// Rollback tracked changes. 
        /// </summary>
        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        /// <summary>
        /// Execute specific query with underliying persistence store
        /// </summary>
        /// <typeparam name="TEntity">Entity type to map query results</typeparam>
        /// <param name="sqlQuery">
        /// Dialect Query 
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        /// </param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>
        /// Enumerable results 
        /// </returns>
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        /// <summary>
        /// Execute arbitrary command into underliying persistence store
        /// </summary>
        /// <param name="sqlCommand">
        /// Command to execute
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        ///</param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>The number of affected records</returns>
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }
    }
}
