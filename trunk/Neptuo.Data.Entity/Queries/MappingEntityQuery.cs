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

        public bool Any()
        {
            return Items.Any();
        }

        public int Count()
        {
            return Items.Count();
        }

        #region ResultSingle

        public TBusiness ResultSingle()
        {
            var source = (IQueryable<TEntity>)Items;
            return AppendWhere(source, null, null).FirstOrDefault();
        }

        public TBusiness ResultSingle(int index)
        {
            var source = (IQueryable<TEntity>)Items;
            return AppendWhere(source, index, 1).FirstOrDefault();
        }

        #endregion

        #region Result

        public IQueryResult<TBusiness> Result()
        {
            var source = (IQueryable<TEntity>)Items;

            var items = AppendWhere(source, null, null);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TBusiness>(items, items.Count());
        }

        public IQueryResult<TTarget> Result<TTarget>(Expression<Func<TBusiness, TTarget>> projection)
        {
            var source = (IQueryable<TEntity>)Items;

            var items = AppendWhere(source, null, null).Select(projection);
            var originalItems = AppendWhere(source, null, null);//.WithTranslations();
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TTarget>(items, originalItems.Count());
        }

        #endregion

        #region PageResult

        public IQueryResult<TBusiness> PageResult(int pageIndex, int pageSize)
        {
            var source = (IQueryable<TEntity>)Items;

            var items = AppendWhere(source, pageIndex, pageSize);
            var originalItems = AppendWhere(source, null, null);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TBusiness>(items, originalItems.Count());
        }

        public IQueryResult<TTarget> PageResult<TTarget>(Expression<Func<TBusiness, TTarget>> projection, int pageIndex, int pageSize)
        {
            var source = (IQueryable<TEntity>)Items;

            var items = AppendWhere(source, pageIndex, pageSize).Select(projection);
            var originalItems = AppendWhere(source, null, null);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TTarget>(items, originalItems.Count());
        }

        #endregion

        #region EnumerateItems

        public IEnumerable<TBusiness> EnumerateItems()
        {
            var source = (IQueryable<TEntity>)Items;

            var items = AppendWhere(source, null, null);
            return items.ToList();
        }

        public IEnumerable<TTarget> EnumerateItems<TTarget>(Expression<Func<TBusiness, TTarget>> projection)
        {
            var source = (IQueryable<TEntity>)Items;

            var items = AppendWhere(source, null, null).Select(projection);
            return items.ToList();
        }

        public IEnumerable<TBusiness> EnumeratePageItems(int pageIndex, int pageSize)
        {
            var source = (IQueryable<TEntity>)Items;

            var items = AppendWhere(source, pageIndex, pageSize);
            return items.ToList();
        }

        public IEnumerable<TTarget> EnumeratePageItems<TTarget>(Expression<Func<TBusiness, TTarget>> projection, int pageIndex, int pageSize)
        {
            var source = (IQueryable<TEntity>)Items;

            var items = AppendWhere(source, pageIndex, pageSize).Select(projection);
            return items.ToList();
        }

        #endregion
    }
}
