using Neptuo.Data.Queries;
using Neptuo.Linq;
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
    public abstract class MappingEntityQuery<TBusiness, TEntity, TFilter> : EntityQueryBase<TEntity, TFilter>, IQuery<TBusiness, TFilter>
        where TEntity : class, TBusiness
        where TBusiness : IKey<int>
    {
        protected IQueryable<TBusiness> Items { get; private set; }

        protected int? PageSize { get; set; }
        protected int? PageIndex { get; set; }

        public MappingEntityQuery(IQueryable<TEntity> items)
        {
            Guard.NotNull(items, "items");
            Items = items;
        }

        public IQuery<TBusiness, TFilter> OrderBy(Expression<Func<TBusiness, object>> sorter)
        {
            Items = Items.OrderBy(sorter);
            return this;
        }

        public IQuery<TBusiness, TFilter> OrderByDescending(Expression<Func<TBusiness, object>> sorter)
        {
            Items = Items.OrderByDescending(sorter);
            return this;
        }

        public IQueryResult<TBusiness> Result()
        {
            var source = (IQueryable<TEntity>)Items;

            var items = AppendWhere(source, PageIndex, PageSize);
            var originalItems = AppendWhere(source, null, null);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TBusiness>(items, originalItems.Count());
        }

        public IQueryResult<TTarget> Result<TTarget>(Expression<Func<TBusiness, TTarget>> projection)
        {
            var source = (IQueryable<TEntity>)Items;

            var items = AppendWhere(source, PageIndex, PageSize).Select(projection);//.WithTranslations();
            var originalItems = AppendWhere(source, null, null);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TTarget>(items, originalItems.Count());
        }

        public TBusiness ResultSingle()
        {
            return AppendWhere((IQueryable<TEntity>)Items, PageIndex, PageSize).FirstOrDefault();
        }

        public bool Any()
        {
            return Items.Any();
        }

        public int Count()
        {
            return Items.Count();
        }

        public IQuery<TBusiness, TFilter> Page(int? pageIndex, int? pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            return this;
        }

        public IQueryResult<TBusiness> PageResult(int pageIndex, int pageSize)
        {
            return Page(pageIndex, pageSize).Result();
        }
    }
}
