using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Migrations
{
    public class InstanceMigrationService : BaseMigrationService
    {
        private SortedList<DateTime, IMigration> migrations = new SortedList<DateTime, IMigration>();

        public InstanceMigrationService(DateTime fromVersion, DateTime? toVersion = null)
            : base(fromVersion, toVersion ?? DateTime.Now)
        { }

        public void Register(DateTime timestamp, IMigration migration)
        {
            if (timestamp > FromVersion && timestamp < ToVersion)
            {
                migrations.Add(timestamp, migration);
                HasMigrations = true;
            }
        }

        public void Register(IDateTimeMigration migration)
        {
            Register(migration.Timestamp, migration);
        }

        protected override int MigrationCount
        {
            get { return migrations.Count; }
        }

        protected override IMigration GetMigration(int index)
        {
            return migrations.Values[index];
        }
    }
}
