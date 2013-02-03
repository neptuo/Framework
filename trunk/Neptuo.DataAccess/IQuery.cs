using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Neptuo.DataAccess
{
    /// <summary>
    /// Querying repository.
    /// </summary>
    public interface IQuery<TEntity, TFilter>
    {
        IQuery<TEntity, TFilter> OrderBy(Expression<Func<TEntity, object>> sorter);
        IQuery<TEntity, TFilter> OrderByDescending(Expression<Func<TEntity, object>> sorter);
        //IQuery<TEntity, TFilter> Where<TValue>(Expression<Func<TFilter, TValue>> selector, TValue value); // Pouze and!

        IQueryResult<TEntity> Result();
        IQueryResult<TTarget> Result<TTarget>(Expression<Func<TEntity, TTarget>> projection);

        IQuery<TEntity, TFilter> Page(int pageIndex, int pageSize);
        IQueryResult<TEntity> PageResult(int pageIndex, int pageSize);
    }
}
