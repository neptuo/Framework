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
        private readonly InstanceStorage instances;
        private readonly string scopeName;
        private readonly DependencyDefinitionCollection definitions;
        private readonly InstanceResolver resolver;

        /// <summary>
        /// Creates new intance.
        /// </summary>
        public SimpleDependencyContainer()
            : this(DependencyLifetime.RootScopeName, new DependencyRegistry(), new InstanceStorage())
        { }

        private SimpleDependencyContainer(string scopeName, DependencyRegistry registry, InstanceStorage instances)
        {
            Ensure.NotNullOrEmpty(scopeName, "scopeName");
            Ensure.NotNull(registry, "registry");
            Ensure.NotNull(instances, "instances");
            this.scopeName = scopeName;
            this.registry = registry;
            this.instances = instances;

            definitions.Add(typeof(IDependencyContainer), DependencyLifetime.NameScope(scopeName), this);
            definitions.Add(typeof(IDependencyProvider), DependencyLifetime.NameScope(scopeName), this);
        }

        #region IDependencyContainer

        public IDependencyDefinitionCollection Definitions
        {
            get { return definitions; }
        }

        #endregion

        #region IDependencyProvider

        public string ScopeName
        {
            get { return scopeName; }
        }
        
        IDependencyDefinitionReadOnlyCollection IDependencyProvider.Definitions
        {
            get { return definitions; }
        }

        public IDependencyContainer Scope(string scopeName)
        {
            return new SimpleDependencyContainer(
                scopeName, 
                new DependencyRegistry(registry.CopyRegistries()), 
                new InstanceStorage(
                    instances.CopyObjects(new List<string>()),
                    instances.CopyActivators(new List<string>())
                ) //TODO: SKip scoped instances.
            );
        }

        public object Resolve(Type requiredType)
        {
            return resolver.Resolve(requiredType);
        }

        #endregion

    }
}
