using Neptuo.Data.Queries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity.Queries
{
    public class EntityQuery<TEntity> : IQuery<TEntity, TEntity>
        where TEntity : IKey<Key>
    {
        protected IQueryable<TEntity> OriginalItems { get; private set; }
        protected IQueryable<TEntity> Items { get; private set; }

        public EntityQuery(IQueryable<TEntity> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            OriginalItems = items;
            Items = items;
        }

        public TEntity Filter { get; private set; }

        public IQuery<TEntity, TEntity> OrderBy(Expression<Func<TEntity, object>> sorter)
        {
            Items = Items.OrderBy(sorter);
            return this;
        }

        public IQuery<TEntity, TEntity> OrderByDescending(Expression<Func<TEntity, object>> sorter)
        {
            Items = Items.OrderByDescending(sorter);
            return this;
        }

        public IQueryResult<TEntity> Result()
        {
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(Items.ToString());
            return new EntityQueryResult<TEntity>(Items, OriginalItems.Count());
        }

        public IQueryResult<TTarget> Result<TTarget>(Expression<Func<TEntity, TTarget>> projection)
        {
            var items = Items.Select(projection);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TTarget>(items, OriginalItems.Count());
        }

        public TEntity ResultSingle()
        {
            return Items.FirstOrDefault();
        }

        public bool Any()
        {
            return Items.Any();
        }

        public int Count()
        {
            return Items.Count();
        }

        public IQuery<TEntity, TEntity> Page(int pageIndex, int pageSize)
        {
            Items = Items.Skip(pageIndex * pageSize).Take(pageSize);
            return this;
        }

        public IQueryResult<TEntity> PageResult(int pageIndex, int pageSize)
        {
            return Page(pageIndex, pageSize).Result();
        }
    }
}
