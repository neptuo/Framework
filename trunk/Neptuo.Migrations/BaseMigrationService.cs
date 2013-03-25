using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Migrations
{
    public class BaseMigrationService : IMigrationService
    {
        private SortedList<DateTime, IMigration> migrations = new SortedList<DateTime, IMigration>();

        public bool IsExecuted { get; protected set; }
        public bool HasMigrations { get; protected set; }

        internal DateTime FromVersion { get; private set; }
        internal DateTime ToVersion { get; private set; }

        public event EventHandler<MigrationServiceExecutedEventArgs> OnExecuted;

        public BaseMigrationService(DateTime fromVersion, DateTime? toVersion = null)
        {
            FromVersion = fromVersion;
            ToVersion = toVersion ?? DateTime.Now;
        }

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

        public void Execute()
        {
            if (IsExecuted)
                return;

            foreach (KeyValuePair<DateTime, IMigration> migration in migrations)
                migration.Value.Execute();

            if (OnExecuted != null)
                OnExecuted(this, new MigrationServiceExecutedEventArgs(true));

            IsExecuted = true;
            HasMigrations = false;
        }
    }

    public class MigrationServiceExecutedEventArgs : EventArgs
    {
        public bool Success { get; private set; }

        public MigrationServiceExecutedEventArgs(bool success)
        {
            Success = success;
        }
    }
}
