using Neptuo.Lifetimes.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ObjectBuilder.Client
{
    public class DependencyContainer : IDependencyContainer, ILifetimeMapping<IDependencyLifetime>
    {
        protected bool IsChildMappingCreated { get; set; }
        protected DependencyRegistry Registry { get; set; }
        protected LifetimeMapping<IDependencyLifetime> LifetimeMapping { get; private set; }

        public DependencyContainer()
            : this(new DependencyRegistry(), new LifetimeMapping<IDependencyLifetime>())
        {
            IsChildMappingCreated = true;
        }

        public DependencyContainer(DependencyRegistry registry, LifetimeMapping<IDependencyLifetime> lifetimeMapping)
        {
            if (registry == null)
                throw new ArgumentNullException("registry");

            if (lifetimeMapping == null)
                throw new ArgumentNullException("lifetimeMapping");

            Registry = registry;
            LifetimeMapping = lifetimeMapping;
        }

        public IDependencyContainer RegisterInstance(Type t, string name, object instance)
        {
            //Registry.Add(GetKey(t), name, new DependencyRegistryItem
            //{
            //    Target = t,
            //    Constructor = null,
            //    LifetimeManager = new SingletonLifetimeManager(instance)
            //});
            //return this;
            throw new NotImplementedException();
        }

        public IDependencyContainer RegisterType(Type from, Type to, string name, object lifetime)
        {
            //Registry.Add(GetKey(from), name, new DependencyRegistryItem
            //{
            //    Target = to,
            //    Constructor = FindBestConstructor(to),
            //    LifetimeManager = MapLifetime(lifetime)
            //});
            //return this;
            throw new NotImplementedException();
        }

        public IDependencyContainer CreateChildContainer()
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type t, string name)
        {
            string key = GetKey(t);
            DependencyRegistryItem item = Registry.GetByKey(key, name);
            if (item != null)
                return Build(item);

            throw new NotImplementedException();
            //TODO: Implement registering class
        }

        public IEnumerable<object> ResolveAll(Type t)
        {
            List<object> result = new List<object>();
            string key = GetKey(t);

            foreach (DependencyRegistryItem item in Registry.GetAllByKey(key))
                result.Add(Build(item));

            return result;
        }

        private string GetKey(Type t)
        {
            return t.FullName;
        }

        private object Build(DependencyRegistryItem item)
        {
            if (item == null)
                return null;

            object instance = item.LifetimeManager.GetValue();
            if (instance != null)
                return instance;

            //TODO: Implement creating instance
            throw new NotImplementedException();
        }

        private IDependencyLifetime MapLifetime(object lifetime)
        {
            IDependencyLifetime lifetimeManager = null;
            if (lifetime != null)
                lifetimeManager = LifetimeMapping.Resolve(lifetime);
            else
                lifetimeManager = new TransientLifetimeManager();

            return lifetimeManager;
        }

        private ConstructorInfo FindBestConstructor(Type type)
        {
            ConstructorInfo result = null;
            foreach (ConstructorInfo ctor in type.GetConstructors())
            {
                if (result == null)
                    result = ctor;
                else if (result.GetParameters().Length < ctor.GetParameters().Length)
                    result = ctor;
            }
            return result;
        }

        public void Map(Type lifetimeType, ILifetimeMapper<IDependencyLifetime> mapper)
        {
            if (!IsChildMappingCreated)
            {
                LifetimeMapping = LifetimeMapping.CreateChildMapping();
                IsChildMappingCreated = true;
            }

            LifetimeMapping.Map(lifetimeType, mapper);
        }
    }
}
