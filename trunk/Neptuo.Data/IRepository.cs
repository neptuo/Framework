using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    /// <summary>
    /// Base repository for CRUD operations on entities of type <typeparamref name="TEntity"/>.
    /// Entity primary key is of type <typeparamref name="TKey"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TKey">Entity key type.</typeparam>
    public interface IRepository<TEntity, TKey>
        where TEntity : class
    {
        /// <summary>
        /// Finds all entities.
        /// </summary>
        /// <returns>All entities.</returns>
        IQueryable<TEntity> Get();

        /// <summary>
        /// Finds single result by <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Entity key.</param>
        /// <returns>Single object or null.</returns>
        TEntity Get(TKey id);

        /// <summary>
        /// Inserts new item to repository.
        /// </summary>
        /// <param name="item">New item.</param>
        void Insert(TEntity item);

        /// <summary>
        /// Updates existing item in repository.
        /// </summary>
        /// <param name="item">Item to update.</param>
        void Update(TEntity item);

        /// <summary>
        /// Deletes item from repository.
        /// </summary>
        /// <param name="item">Item to delete.</param>
        void Delete(TEntity item);
    }

    /// <summary>
    /// Repository that uses <code>int</code> as primary key for entity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, Key>
        where TEntity : class, IKey<Key>, IVersion
    { }
}
