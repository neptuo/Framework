using Neptuo.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Migrations
{
    public abstract class BaseMigrationProvider : IMigrationProvider
    {
        protected bool MigrationsFound { get; private set; }
        protected IMigrationActivator Activator { get; private set; }
        protected SortedList<DateTime, Type> Migrations { get; private set; }
        public DateTime Timestamp { get; protected set; }

        private DateTime? target = null;

        public BaseMigrationProvider(IMigrationActivator activator)
        {
            Activator = activator;
            Migrations = new SortedList<DateTime, Type>();
        }

        public IMigrationService FindUpMigrations()
        {
            return FindUpMigrations(DateTime.Now);
        }

        public IMigrationService FindUpMigrations(DateTime target)
        {
            if (!MigrationsFound)
            {
                RegisterMigrations();
                MigrationsFound = true;
            }
            BaseMigrationService service = CreateService();
            Timestamp = LoadLastTimestamp();
            RegisterActiveUpMigrations(service, target);

            if (service.HasMigrations)
            {
                this.target = target;
                service.OnExecuted += OnExecuted;
            }
            else
            {
                UpdateLastTimestamp(target);
            }

            return service;
        }

        public IMigrationService FindDownMigrations(DateTime target)
        {
            if (!MigrationsFound)
            {
                RegisterMigrations();
                MigrationsFound = true;
            }
            BaseMigrationService service = CreateService();
            Timestamp = LoadLastTimestamp();
            RegisterActiveDownMigrations(service, target);

            if (service.HasMigrations)
            {
                this.target = target;
                service.OnExecuted += OnExecuted;
            }
            else
            {
                UpdateLastTimestamp(target);
            }

            return service;
        }

        protected virtual void OnExecuted(object sender, MigrationServiceExecutedEventArgs e)
        {
            if (e.Success)
            {
                UpdateLastTimestamp(target.Value);
                target = null;
                ((BaseMigrationService)sender).OnExecuted -= OnExecuted;
            }
        }

        private void UpdateLastTimestamp(DateTime target)
        {
            Timestamp = target;
            SaveLastTimestamp(Timestamp);
        }

        protected virtual IEnumerable<Type> FindMigrationTypes()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                yield return type;

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GetCustomAttributes(typeof(MigrationsAttribute), true).Length == 1)
                {
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types)
                        yield return type;
                }
            }
        }

        protected void RegisterMigrations()
        {
            foreach (Type type in FindMigrationTypes())
            {
                if (type.GetInterfaces().Contains(typeof(IMigration)) && !type.IsAbstract && !type.IsInterface)
                    RegisterMigration(type);
            }
        }

        protected virtual void RegisterMigration(Type type)
        {
            MigrationAttribute attribute = ReflectionHelper.GetAttribute<MigrationAttribute>(type);
            if (attribute != null)
            {
                Migrations.Add(attribute.Timestamp, type);
                return;
            }

            IMigration migration = Activator.Activate(type);
            if (migration is IDateTimeMigration)
            {
                Migrations.Add(((IDateTimeMigration)migration).Timestamp, type);
                return;
            }

            throw new MigrationException("Can't find timestamp in migration!");
        }

        protected virtual BaseMigrationService CreateService()
        {
            return new BaseMigrationService(Timestamp);
        }

        protected virtual void RegisterActiveUpMigrations(BaseMigrationService service, DateTime target)
        {
            foreach (KeyValuePair<DateTime, Type> migration in Migrations)
            {
                if (migration.Key > Timestamp && migration.Key < target)
                    service.Register(migration.Key, Activator.Activate(migration.Value));
            }
        }

        protected virtual void RegisterActiveDownMigrations(BaseMigrationService service, DateTime target)
        {
            foreach (KeyValuePair<DateTime, Type> migration in Migrations)
            {
                if (migration.Key > target && migration.Key < Timestamp)
                    service.Register(migration.Key, Activator.Activate(migration.Value));
            }
        }

        protected abstract DateTime LoadLastTimestamp();

        protected abstract void SaveLastTimestamp(DateTime lastTimestamp);
    }
}
