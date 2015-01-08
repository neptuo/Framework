using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Queries
{
    public interface IQueryResult<TResult>
    {
        IEnumerable<TResult> Items { get; }
        int TotalCount { get; }
    }
}
