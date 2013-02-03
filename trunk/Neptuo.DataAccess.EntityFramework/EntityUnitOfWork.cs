using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.DataAccess.EntityFramework
{
    public class EntityUnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;

        public UnitOfWorkState State { get; protected set; }

        public EntityUnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
            State = UnitOfWorkState.Started;
        }

        public void Commit()
        {
            if (State != UnitOfWorkState.Started)
                throw new EntityUnitOfWorkException("Unit of work can commit only from started state!");

            dbContext.SaveChanges();
            State = UnitOfWorkState.Commited;
        }

        public void Dispose()
        {
            State = UnitOfWorkState.Disposed;
        }
    }

    public enum UnitOfWorkState
    {
        Started, Commited, Disposed
    }
}
