using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.DataAccess
{
    public interface IQueryResult<TEntity>
    {
        IEnumerable<TEntity> Items { get; }
        int TotalCount { get; }
    }
}
