using Neptuo.Data.Queries;
using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity.Queries
{
    public abstract class EntityQuery<TEntity, TFilter> : EntityQueryBase<TEntity, TFilter>, IQuery<TEntity, TFilter>
    {
        protected IQueryable<TEntity> Items { get; private set; }

        public EntityQuery(IQueryable<TEntity> items)
        {
            Guard.NotNull(items, "items");
            Items = items;
        }

        public IQuery<TEntity, TFilter> OrderBy(Expression<Func<TEntity, object>> sorter)
        {
            Items = Items.OrderBy(sorter);
            return this;
        }

        public IQuery<TEntity, TFilter> OrderByDescending(Expression<Func<TEntity, object>> sorter)
        {
            Items = Items.OrderByDescending(sorter);
            return this;
        }

        public bool Any()
        {
            return Items.Any();
        }

        public int Count()
        {
            return Items.Count();
        }

        #region ResultSingle

        public TEntity ResultSingle()
        {
            return AppendWhere(Items, null, null).FirstOrDefault();
        }

        public TEntity ResultSingle(int index)
        {
            return AppendWhere(Items, index, 1).FirstOrDefault();
        }

        #endregion

        #region Result

        public IQueryResult<TEntity> Result()
        {
            var items = AppendWhere(Items, null, null);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TEntity>(items, items.Count());
        }

        public IQueryResult<TTarget> Result<TTarget>(Expression<Func<TEntity, TTarget>> projection)
        {
            var items = AppendWhere(Items, null, null).Select(projection);
            //var originalItems = AppendWhere(Items, null, null);//.WithTranslations();
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TTarget>(items, items.Count());
        }

        #endregion

        #region PageResult

        public IQueryResult<TEntity> PageResult(int pageIndex, int pageSize)
        {
            var items = AppendWhere(Items, pageIndex, pageSize);
            var originalItems = AppendWhere(Items, null, null);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TEntity>(items, originalItems.Count());
        }

        public IQueryResult<TTarget> PageResult<TTarget>(Expression<Func<TEntity, TTarget>> projection, int pageIndex, int pageSize)
        {
            var items = AppendWhere(Items, pageIndex, pageSize).Select(projection);
            var originalItems = AppendWhere(Items, null, null);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TTarget>(items, originalItems.Count());
        }

        #endregion

        #region EnumerateItems

        public IEnumerable<TEntity> EnumerateItems()
        {
            var items = AppendWhere(Items, null, null);
            return items.ToList();
        }

        public IEnumerable<TTarget> EnumerateItems<TTarget>(Expression<Func<TEntity, TTarget>> projection)
        {
            var items = AppendWhere(Items, null, null).Select(projection);
            return items.ToList();
        }

        public IEnumerable<TEntity> EnumeratePageItems(int pageIndex, int pageSize)
        {
            var items = AppendWhere(Items, pageIndex, pageSize);
            return items.ToList();
        }

        public IEnumerable<TTarget> EnumeratePageItems<TTarget>(Expression<Func<TEntity, TTarget>> projection, int pageIndex, int pageSize)
        {
            var items = AppendWhere(Items, pageIndex, pageSize).Select(projection);
            return items.ToList();
        }

        #endregion
    }
}
