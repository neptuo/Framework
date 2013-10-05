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
    public abstract class EntityQuery<TEntity, TFilter> : IQuery<TEntity, TFilter>
        where TEntity : IKey<Key>
    {
        protected IQueryable<TEntity> OriginalItems { get; private set; }
        protected IQueryable<TEntity> Items { get; private set; }
        protected Dictionary<string, object> WhereFilters { get; private set; }

        public EntityQuery(IQueryable<TEntity> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            OriginalItems = items;
            Items = items;
            WhereFilters = new Dictionary<string, object>();
        }

        public TFilter Filter { get; private set; }

        public IQuery<TEntity, TFilter> Where<TValue>(Expression<Func<TFilter, TValue>> selector, TValue value)
        {
            WhereFilters[TypeHelper.PropertyName(selector)] = value;
            return this;
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
            var items = AppendWhere(Items, WhereFilters);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TEntity>(items, OriginalItems.Count());
        }

        public IQueryResult<TTarget> Result<TTarget>(Expression<Func<TEntity, TTarget>> projection)
        {
            var items = AppendWhere(Items, WhereFilters).Select(projection);
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

        public IQuery<TEntity, TFilter> Page(int pageIndex, int pageSize)
        {
            Items = Items.Skip(pageIndex * pageSize).Take(pageSize);
            return this;
        }

        public IQueryResult<TEntity> PageResult(int pageIndex, int pageSize)
        {
            return Page(pageIndex, pageSize).Result();
        }

        protected virtual IQueryable<TEntity> AppendWhere(IQueryable<TEntity> items, Dictionary<string, object> whereFilters)
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

        protected abstract Expression BuildWhereExpression(Expression parameter, Dictionary<string, object> whereFilters);
    }
}
