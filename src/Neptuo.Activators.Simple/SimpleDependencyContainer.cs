using Neptuo.Activators.Internals;
using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Base reflection (and #kit compliant) implementation of <see cref="IDependencyContainer"/>.
    /// </summary>
    public class SimpleDependencyContainer : DisposableBase, IDependencyContainer
    {
        private readonly DependencyRegistry registry;
        private readonly string scopeName;

        public string ScopeName
        {
            get { return scopeName; }
        }
        
        /// <summary>
        /// Creates new intance.
        /// </summary>
        public SimpleDependencyContainer()
            : this(DependencyLifetime.RootScopeName, new DependencyRegistry())
        { }

        private SimpleDependencyContainer(string scopeName, DependencyRegistry registry)
        {
            Guard.NotNullOrEmpty(scopeName, "scopeName");
            Guard.NotNull(registry, "registry");
            this.scopeName = scopeName;
            this.registry = registry;

            Map(typeof(IDependencyContainer), DependencyLifetime.NamedScope(scopeName), this);
            Map(typeof(IDependencyProvider), DependencyLifetime.NamedScope(scopeName), this);
        }

        public IDependencyContainer Map(Type requiredType, DependencyLifetime lifetime, object target)
        {
            if (requiredType.IsInterface)
                throw new DependencyException(String.Format("Target can't be interface. Mapping '{0}' to '{1}'.", requiredType.FullName, to.FullName));

            if (requiredType.IsAbstract)
                throw new DependencyException(String.Format("Target can't be abstract class. Mapping '{0}' to '{1}'.", requiredType.FullName, to.FullName));

            registry.Add(GetKey(requiredType), new DependencyRegistryItem
            {
                RequiredType = requiredType,
                Constructor = FindBestConstructor(to),
                Lifetime = lifetime
            });
            
            return this;
        }

        public IDependencyContainer Scope(string scopeName)
        {
            return new SimpleDependencyContainer(scopeName, new DependencyRegistry(registry.CopyRegistries()));
        }

        public object Resolve(Type requiredType)
        {
            string key = GetKey(requiredType);
            DependencyRegistryItem item = registry.GetByKey(key);
            if (item == null)
            {
                Map(requiredType, DependencyLifetime.Transient, requiredType);
                item = registry.GetByKey(key);
            }

            return Build(item);
        }

        private string GetKey(Type t)
        {
            return t.FullName;
        }

        private object Build(DependencyRegistryItem item)
        {
            if (item == null)
                return null;

            object instance = item.Lifetime.GetValue();
            if (instance != null)
                return instance;

            if (item.Constructor == null)
                throw new DependencyException("Missing constructor.");

            ParameterInfo[] parameterDefinitions = item.Constructor.GetParameters();
            object[] parameters = new object[parameterDefinitions.Length];
            for (int i = 0; i < parameterDefinitions.Length; i++)
                parameters[i] = Resolve(parameterDefinitions[i].ParameterType);

            instance = item.Constructor.Invoke(parameters);
            item.Lifetime.SetValue(instance);
            return instance;
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
    }
}
