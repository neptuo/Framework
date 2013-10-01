using Neptuo.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity.Queries
{
    public class EntityQueryResult<TEntity> : IQueryResult<TEntity>
    {
        protected IEnumerable<TEntity> ItemsInternal { get; private set; }
        protected int TotalCountInternal { get; private set; }

        public EntityQueryResult(IEnumerable<TEntity> items, int totalCount)
        {
            ItemsInternal = items;
            TotalCountInternal = totalCount;
        }

        public IEnumerable<TEntity> Items
        {
            get { return ItemsInternal; }
        }

        public int TotalCount
        {
            get { return TotalCountInternal; }
        }
    }
}
