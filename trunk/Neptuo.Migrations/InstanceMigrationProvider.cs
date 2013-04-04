using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Migrations
{
    public abstract class InstanceMigrationProvider : BaseMigrationProvider<InstanceMigrationService>
    {
        public InstanceMigrationProvider(IMigrationActivator activator)
            : base(activator)
        { }

        protected override InstanceMigrationService CreateService()
        {
            return new InstanceMigrationService(Timestamp);
        }

        protected override void RegisterMigrationToService(InstanceMigrationService service, DateTime dateTime, Type migration)
        {
            service.Register(dateTime, Activator.Activate(migration));
        }

        protected override DateTime GetMigrationDateTime(Type migrationType)
        {
            IMigration migration = Activator.Activate(migrationType);
            if (migration is IDateTimeMigration)
                return ((IDateTimeMigration)migration).Timestamp;

            return base.GetMigrationDateTime(migrationType);
        }
    }
}
