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
        where TEntity : IKey<int>
    {
        protected IQueryable<TEntity> Items { get; private set; }

        protected int? PageSize { get; set; }
        protected int? PageIndex { get; set; }

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

        public IQueryResult<TEntity> Result()
        {
            var items = AppendWhere(Items, PageIndex, PageSize);
            var originalItems = AppendWhere(Items, null, null);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TEntity>(items, originalItems.Count());
        }

        public IQueryResult<TTarget> Result<TTarget>(Expression<Func<TEntity, TTarget>> projection)
        {
            var items = AppendWhere(Items, PageIndex, PageSize).Select(projection);
            var originalItems = AppendWhere(Items, null, null);//.WithTranslations();
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TTarget>(items, originalItems.Count());
        }

        public TEntity ResultSingle()
        {
            return AppendWhere(Items, PageIndex, PageSize).FirstOrDefault();
        }

        public bool Any()
        {
            return Items.Any();
        }

        public int Count()
        {
            return Items.Count();
        }

        public IQuery<TEntity, TFilter> Page(int? pageIndex, int? pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            return this;
        }

        public IQueryResult<TEntity> PageResult(int pageIndex, int pageSize)
        {
            return Page(pageIndex, pageSize).Result();
        }
    }
}
