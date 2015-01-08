using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public static class QueryExtensions
    {
        #region PageResult

        public static IQueryResult<TEntity> PageResult<TEntity, TFilter>(this IQuery<TEntity, TFilter> query, int? pageIndex, int? pageSize)
        {
            if (pageSize != null)
                return query.PageResult(pageIndex ?? 0, pageSize.Value);

            return query.Result();
        }

        public static IQueryResult<TTarget> PageResult<TEntity, TFilter, TTarget>(this IQuery<TEntity, TFilter> query, Expression<Func<TEntity, TTarget>> projection, int? pageIndex, int? pageSize)
        {
            if (pageSize != null)
                return query.PageResult(projection, pageIndex ?? 0, pageSize.Value);

            return query.Result(projection);
        }

        #endregion

        #region EnumerateItems

        public static IEnumerable<TEntity> EnumeratePageItems<TEntity, TFilter>(this IQuery<TEntity, TFilter> query, int? pageIndex, int? pageSize)
        {
            if (pageSize != null)
                return query.EnumeratePageItems(pageIndex ?? 0, pageSize.Value);

            return query.EnumerateItems();
        }

        public static IEnumerable<TTarget> EnumeratePageItems<TEntity, TFilter, TTarget>(this IQuery<TEntity, TFilter> query, Expression<Func<TEntity, TTarget>> projection, int? pageIndex, int? pageSize)
        {
            if (pageSize != null)
                return query.EnumeratePageItems(projection, pageIndex ?? 0, pageSize.Value);

            return query.EnumerateItems(projection);
        }

        #endregion
    }
}
