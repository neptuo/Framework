using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.DataAccess.EntityFramework
{
    public class Query<TEntity> : IQuery<TEntity, TEntity>
    {
        private int pageIndex = 0;
        private int pageSize = 50;
        private IQueryable<TEntity> innerQuery;

        public Query(IQueryable<TEntity> innerQuery)
        {
            this.innerQuery = innerQuery;
        }

        public Query(IQueryable<TEntity> innerQuery, int pageSize)
        {
            this.innerQuery = innerQuery;
            this.pageSize = pageSize;
        }

        public IQuery<TEntity, TEntity> OrderBy(Expression<Func<TEntity, object>> sorter)
        {
            innerQuery.OrderBy(sorter);
            return this;
        }

        public IQuery<TEntity, TEntity> OrderByDescending(Expression<Func<TEntity, object>> sorter)
        {
            innerQuery.OrderByDescending(sorter);
            return this;
        }

        //public IQuery<TEntity, TEntity> Where<TValue>(Expression<Func<TEntity, TValue>> filter, TValue value)
        //{
            
        //    //innerQuery.Where(filter = value);
        //    return this;
        //}

        public IQueryResult<TEntity> Result()
        {
            return new QueryResult<TEntity>(
                innerQuery.Skip(pageIndex * pageSize).Take(pageSize).ToList(),
                innerQuery.Count()
            );
        }

        public IQueryResult<TTarget> Result<TTarget>(Expression<Func<TEntity, TTarget>> projection)
        {
            return new QueryResult<TTarget>(
                innerQuery.Skip(pageIndex * pageSize).Take(pageSize).Select(projection).ToList(), 
                innerQuery.Count()
            );
        }

        public IQuery<TEntity, TEntity> Page(int pageIndex, int pageSize)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            return this;
        }

        public IQueryResult<TEntity> PageResult(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
