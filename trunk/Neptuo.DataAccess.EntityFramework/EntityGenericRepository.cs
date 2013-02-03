using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.DataAccess.EntityFramework
{
    /// <summary>
    /// Generic repository for use with EntityFramework.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TKey">Entity primary key type.</typeparam>
    /// <typeparam name="TContext">DbContext used for storing objects.</typeparam>
    public abstract class EntityGenericRepository<TEntity, TKey, TContext> : IRepository<TEntity, TKey>
        where TKey : struct
        where TEntity : class
        where TContext : DbContext, new()
    {
        /// <summary>
        /// Current dbContext.
        /// </summary>
        public TContext DbContext { get; protected set; }

        public EntityGenericRepository(TContext dbContext)
        {
            DbContext = dbContext;
        }

        #region Get/Insert/Update/Delete

        /// <summary>
        /// Finds single result by <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Entity key.</param>
        /// <returns>Single object or null.</returns>
        public TEntity Get(TKey id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Inserts new item to repository.
        /// </summary>
        /// <param name="item">New item.</param>
        public void Insert(TEntity item)
        {
            DbContext.Set<TEntity>().Add(item);
        }

        /// <summary>
        /// Updates existing item in repository.
        /// </summary>
        /// <param name="item">Item to update.</param>
        public void Update(TEntity item)
        {
            DbContext.Set<TEntity>().Attach(item);
            DbContext.Entry<TEntity>(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes item from repository.
        /// </summary>
        /// <param name="item">Item to delete.</param>
        public void Delete(TEntity item)
        {
            DbContext.Set<TEntity>().Remove(item);
        }

        #endregion
    }

    /// <summary>
    /// Generic repository using <see cref="int"/> as entity primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TContext">DbContext used for storing objects.</typeparam>
    public class EntityGenericRepository<TEntity, TContext> : EntityGenericRepository<TEntity, int, TContext>, IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext, new()
    {
        public EntityGenericRepository(TContext dbContext)
            : base(dbContext)
        { }
    }

}
