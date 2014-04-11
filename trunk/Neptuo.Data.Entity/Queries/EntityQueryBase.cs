using Neptuo.Data.Queries;
using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity.Queries
{
    public abstract class EntityQueryBase<TEntity, TFilter>
        where TEntity : IKey<int>
    {
        private TFilter filter;

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

        protected virtual IQueryable<TEntity> AppendWhere(IQueryable<TEntity> items, int? pageIndex, int? pageSize)
        {
            if(pageSize != null)
                items = items.Skip((pageIndex ?? 0) * pageSize.Value).Take(pageSize.Value);

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
