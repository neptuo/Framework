using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Neptuo.Data.Queries
{
    public interface IQuery<TResult, TFilter>
    {
        TFilter Filter { get; set; }

        IQuery<TResult, TFilter> OrderBy(Expression<Func<TResult, object>> sorter);
        IQuery<TResult, TFilter> OrderByDescending(Expression<Func<TResult, object>> sorter);

        bool Any();
        int Count();

        TResult ResultSingle();
        TResult ResultSingle(int index);

        IQueryResult<TResult> Result();
        IQueryResult<TTarget> Result<TTarget>(Expression<Func<TResult, TTarget>> projection);

        IQueryResult<TResult> PageResult(int pageIndex, int pageSize);
        IQueryResult<TTarget> PageResult<TTarget>(Expression<Func<TResult, TTarget>> projection, int pageIndex, int pageSize);

        IEnumerable<TResult> EnumerateItems();
        IEnumerable<TTarget> EnumerateItems<TTarget>(Expression<Func<TResult, TTarget>> projection);
        IEnumerable<TTarget> EnumeratePageItems<TTarget>(Expression<Func<TResult, TTarget>> projection, int pageIndex, int pageSize);
    }
}
