using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity
{
    public class DbContextUnitOfWorkFactory : IUnitOfWorkFactory
    {
        protected DbContext DbContext { get; private set; }

        public DbContextUnitOfWorkFactory(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IUnitOfWork Create()
        {
            return new DbContextUnitOfWork(DbContext);
        }
    }
}
