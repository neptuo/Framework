using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity
{
    /// <summary>
    /// Generic repository for use with EntityFramework.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TKey">Entity primary key type.</typeparam>
    /// <typeparam name="TContext">DbContext used for storing objects.</typeparam>
    public abstract class EntityRepository<TEntity, TKey, TContext> : IRepository<TEntity, TKey>
        where TEntity : class
        where TContext : IDbContext
    {
        /// <summary>
        /// Current dbContext.
        /// </summary>
        public TContext DbContext { get; protected set; }

        public EntityRepository(TContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            DbContext = dbContext;
        }

        #region Get/Insert/Update/Delete

        /// <summary>
        /// Finds single result by <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Entity key.</param>
        /// <returns>Single object or null.</returns>
        public virtual TEntity Get(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return DbContext.Set<TEntity>().Find(key);
        }

        /// <summary>
        /// Inserts new item to repository.
        /// </summary>
        /// <param name="item">New item.</param>
        public virtual void Insert(TEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            DbContext.Set<TEntity>().Add(item);
        }

        /// <summary>
        /// Updates existing item in repository.
        /// </summary>
        /// <param name="item">Item to update.</param>
        public virtual void Update(TEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            DbContext.Set<TEntity>().Attach(item);
            DbContext.Entry<TEntity>(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes item from repository.
        /// </summary>
        /// <param name="item">Item to delete.</param>
        public virtual void Delete(TEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            DbContext.Set<TEntity>().Remove(item);
        }

        #endregion
    }

    /// <summary>
    /// Generic repository using <see cref="Key"/> as entity primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TContext">DbContext used for storing objects.</typeparam>
    public class EntityRepository<TEntity, TContext> : EntityRepository<TEntity, Key, TContext>, IRepository<TEntity>
        where TEntity : class, IKey<Key>, IVersion
        where TContext : IDbContext
    {
        public EntityRepository(TContext dbContext)
            : base(dbContext)
        { }

        public override TEntity Get(Key key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return DbContext.Set<TEntity>().Find(key.ID);
        }

        public override void Update(TEntity item)
        {
            try
            {
                base.Update(item);
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new IncorrectVersionException(item.Key);
            }
        }

        public override void Delete(TEntity item)
        {
            try
            {
                base.Delete(item);
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new IncorrectVersionException(item.Key);
            }
        }

        protected void CheckVersion(TEntity item)
        {
            //TEntity current = Get(item.Key);
            //if (current.Version != item.Version)
            //    throw new IncorrectVersionException(item.Key);
        }
    }
}
