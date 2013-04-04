using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Migrations
{
    public abstract class BaseMigrationService : IMigrationService
    {
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

        public void Execute()
        {
            if (IsExecuted)
                return;

            for (int i = 0; i < MigrationCount; i++)
            {
                IMigration migration = GetMigration(i);
                if (migration != null)
                    migration.Execute();
            }

            if (OnExecuted != null)
                OnExecuted(this, new MigrationServiceExecutedEventArgs(true));

            IsExecuted = true;
            HasMigrations = false;
        }

        protected abstract int MigrationCount { get; }

        protected abstract IMigration GetMigration(int index);
    }
}
