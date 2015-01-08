using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Migrations
{
    /// <summary>
    /// Creates instances on the fly.
    /// </summary>
    public abstract class LazyMigrationProvider : BaseMigrationProvider<LazyMigrationService>
    {
        public LazyMigrationProvider(IMigrationActivator activator)
            : base(activator)
        { }

        protected override LazyMigrationService CreateService()
        {
            return new LazyMigrationService(Timestamp);
        }

        protected override void RegisterMigrationToService(LazyMigrationService service, DateTime dateTime, Type migration)
        {
            service.Register(dateTime, new Lazy<IMigration>(() => Activator.Activate(migration)));
        }
    }
}
