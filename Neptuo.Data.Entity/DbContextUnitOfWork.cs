using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity
{
    public class DbContextUnitOfWork : IUnitOfWork
    {
        protected DbContext DbContext { get; private set; }

        public DbContextUnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        { }
    }
}
