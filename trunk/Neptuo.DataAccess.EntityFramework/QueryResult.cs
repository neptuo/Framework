using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.DataAccess.EntityFramework
{
    public class QueryResult<TEntity> : IQueryResult<TEntity>
    {
        public IEnumerable<TEntity> Items { get; protected set; }
        public int TotalCount { get; protected set; }

        public QueryResult(IEnumerable<TEntity> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}
