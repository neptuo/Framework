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
        private TFilter filter;

        protected IQueryable<TEntity> OriginalItems { get; private set; }
        protected IQueryable<TEntity> Items { get; private set; }

        public TFilter Filter
        {
            get
            {
                if (filter == null)
                    filter = CreateFilter();

                return filter;
            }
            set { filter = value; }
        }

        public EntityQuery(IQueryable<TEntity> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            OriginalItems = items;
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
            var items = AppendWhere(Items);
            var originalItems = AppendWhere((IQueryable<TEntity>)Items);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TEntity>(items, originalItems.Count());
        }

        public IQueryResult<TTarget> Result<TTarget>(Expression<Func<TEntity, TTarget>> projection)
        {
            var items = AppendWhere(Items).Select(projection);
            var originalItems = AppendWhere((IQueryable<TEntity>)Items);
            //Trace.WriteLine("Neptuo.Data.Entity.Queries.EntityQuery: ");
            //Trace.WriteLine(items.ToString());
            return new EntityQueryResult<TTarget>(items, originalItems.Count());
        }

        public TEntity ResultSingle()
        {
            return AppendWhere(Items).FirstOrDefault();
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

        protected virtual IQueryable<TEntity> AppendWhere(IQueryable<TEntity> items)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity));
            Expression predicate = BuildWhereExpression(parameter);

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

        protected abstract Expression BuildWhereExpression(Expression parameter);

        protected abstract TFilter CreateFilter();
    }
}
