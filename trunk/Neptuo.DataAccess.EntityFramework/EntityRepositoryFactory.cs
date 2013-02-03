using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.DataAccess.EntityFramework
{
    public class EntityRepositoryFactory : IRepositoryFactory
    {
        private DbContext dbContext;
        private IUnitOfWork unitOfWork;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if(unitOfWork == null)
                    throw new EntityUnitOfWorkException("Unit of work not started!");

                return unitOfWork;
            }
        }

        public EntityRepositoryFactory(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IUnitOfWork StartUnitOfWork()
        {
            if (unitOfWork != null)
                throw new EntityUnitOfWorkException("Unit of work already started started!");

            unitOfWork = new EntityUnitOfWork(dbContext);
            return unitOfWork;
        }

        public T Create<T>()
        {
            throw new NotImplementedException();
        }
    }
}
