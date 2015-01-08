using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Queries
{
    public interface IQueryFilter<TFilter>
    {
        IQueryFilter<TFilter> Where<TValue>(Expression<Func<TFilter, TValue>> selector, TValue value);

        IQueryFilter<TFilter> And(IQueryFilter<TFilter> filter);
        IQueryFilter<TFilter> Or(IQueryFilter<TFilter> filter);

        IQueryFilter<TFilter> Bracketed(IQueryFilter<TFilter> filter);
    }
}
