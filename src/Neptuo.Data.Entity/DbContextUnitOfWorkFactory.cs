using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity
{
    public class DbContextUnitOfWorkFactory<TDbContext> : IUnitOfWorkFactory
        where TDbContext : DbContext
    {
        protected TDbContext DbContext { get; private set; }

        public DbContextUnitOfWorkFactory(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IUnitOfWork Create()
        {
            return new DbContextUnitOfWork(DbContext);
        }
    }
}
