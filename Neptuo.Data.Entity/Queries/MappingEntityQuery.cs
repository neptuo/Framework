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
    public abstract class MappingEntityQuery<TBusiness, TEntity, TFilter> : IQuery<TBusiness, TFilter>
        where TEntity : class, TBusiness
        where TBusiness : IKey<Key>
    {
        protected IQueryable<TBusiness> OriginalItems { get; private set; }
        protected IQueryable<TBusiness> Items { get; private set; }
        protected Dictionary<string, IQuerySearch> WhereFilters { get; private set; }

        public MappingEntityQuery(IQueryable<TEntity> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            OriginalItems = items;
            Items = items;
            WhereFilters = new Dictionary<string, IQuerySearch>();
        }

        public TFilter Filter { get; private set; }

        public IQuery<TBusiness, TFilter> Where<TValue>(Expression<Func<TFilter, TValue>> selector, TValue value)
            where TValue : IQuerySearch
        {
            WhereFilters[TypeHelper.PropertyName(selector)] = value;
            return this;
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
            var items = AppendWhere((IQueryable<TEntity>)Items, WhereFilters);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TBusiness>(items, OriginalItems.Count());
        }

        public IQueryResult<TTarget> Result<TTarget>(Expression<Func<TBusiness, TTarget>> projection)
        {
            var items = AppendWhere((IQueryable<TEntity>)Items, WhereFilters).Select(projection).WithTranslations();
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TTarget>(items, OriginalItems.Count());
        }

        public TBusiness ResultSingle()
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

        public IQuery<TBusiness, TFilter> Page(int pageIndex, int pageSize)
        {
            Items = Items.Skip(pageIndex * pageSize).Take(pageSize);
            return this;
        }

        public IQueryResult<TBusiness> PageResult(int pageIndex, int pageSize)
        {
            return Page(pageIndex, pageSize).Result();
        }

        protected virtual IQueryable<TEntity> AppendWhere(IQueryable<TEntity> items, Dictionary<string, IQuerySearch> whereFilters)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity));
            Expression predicate = BuildWhereExpression(parameter, whereFilters);

            if (predicate != null)
            {
                MethodCallExpression whereCallExpression = Expression.Call(
                   typeof(Queryable),
                   TypeHelper.MethodName<IQueryable<TEntity>, Expression<Func<TEntity, bool>>, IQueryable<TEntity>>(q => q.Where),
                   new Type[] { typeof(TEntity) },
                   items.Expression,
                   Expression.Lambda<Func<TEntity, bool>>(predicate, new ParameterExpression[] { parameter })
                );

                return items.Provider.CreateQuery<TEntity>(whereCallExpression);
            }
            return items;
        }

        protected abstract Expression BuildWhereExpression(Expression parameter, Dictionary<string, IQuerySearch> whereFilters);
    }
}
