using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.DataAccess.Collection
{
    /// <summary>
    /// Repostiory wrapper for collection.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TKey">Entity primary key type.</typeparam>
    public class CollectionRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        private ICollection<TEntity> dataSource;
        private Func<TKey, bool> keySelector;

        public CollectionRepository(ICollection<TEntity> dataSource, Func<TKey, bool> keySelector)
        {
            this.dataSource = dataSource;
            this.keySelector = keySelector;
        }

        public TEntity Get(TKey id)
        {
            return dataSource.FirstOrDefault(e => keySelector(id));
        }

        public void Insert(TEntity item)
        {
            dataSource.Add(item);
        }

        public void Update(TEntity item)
        {
            if (dataSource.Contains(item))
                dataSource.Remove(item);

            dataSource.Add(item);
            
        }

        public void Delete(TEntity item)
        {
            dataSource.Remove(item);
        }
    }

    public class CollectionRepository<TEntity> : CollectionRepository<TEntity, int> 
        where TEntity : class
    {
        public CollectionRepository(ICollection<TEntity> dataSource, Func<int, bool> keySelector)
            : base(dataSource, keySelector)
        { }
    }
}
