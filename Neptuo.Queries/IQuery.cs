using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Neptuo.Queries
{
    public interface IQuery<TResult, TFilter>
    {
        IQuery<TResult, TFilter> OrderBy(Expression<Func<TResult, object>> sorter);
        IQuery<TResult, TFilter> OrderByDescending(Expression<Func<TResult, object>> sorter);
        IQueryFilter<TFilter> Where<TValue>(Expression<Func<TFilter, TValue>> selector, TValue value);
        IQueryFilter<TFilter> Where();

        IQueryResult<TResult> Result();
        IQueryResult<TTarget> Result<TTarget>(Expression<Func<TResult, TTarget>> projection);
        TResult ResultSingle();
        bool Any();
        int Count();

        IQuery<TResult, TFilter> Page(int pageIndex, int pageSize);
        IQueryResult<TResult> PageResult(int pageIndex, int pageSize);
    }
}
