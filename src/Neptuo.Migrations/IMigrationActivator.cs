using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Migrations
{
    public interface IMigrationActivator
    {
        IMigration Activate(Type migrationType);
    }

    public static class MigrationActivatorExtensions
    {
        public static IMigration Activate<T>(this IMigrationActivator activator)
            where T : IMigration
        {
            return activator.Activate(typeof(T));
        }
    }
}
