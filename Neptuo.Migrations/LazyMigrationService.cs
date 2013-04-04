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
    public class LazyMigrationService : BaseMigrationService
    {
        private SortedList<DateTime, Lazy<IMigration>> migrations = new SortedList<DateTime, Lazy<IMigration>>();

        public LazyMigrationService(DateTime fromVersion, DateTime? toVersion = null)
            : base(fromVersion, toVersion ?? DateTime.Now)
        { }

        public void Register(DateTime timestamp, Lazy<IMigration> migration)
        {
            if (timestamp > FromVersion && timestamp < ToVersion)
            {
                migrations.Add(timestamp, migration);
                HasMigrations = true;
            }
        }

        protected override int MigrationCount
        {
            get { return migrations.Count; }
        }

        protected override IMigration GetMigration(int index)
        {
            return migrations.Values[index].Value;
        }
    }
}
