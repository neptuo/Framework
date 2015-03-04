using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    public static class _DependencyContainerExtensions
    {
        public interface IDependencyScopeMapping
        {
            IDependencyTargetMapping In(DependencyLifetime lifetime);
        }

        public interface IDependencyTargetMapping
        {
            IDependencyContainer To(object target);
        }

        internal class DependencyRegistration : IDependencyScopeMapping, IDependencyTargetMapping
        {
            private readonly IDependencyContainer dependencyContainer;
            private readonly Type requiredType;
            private DependencyLifetime lifetime;

            public DependencyRegistration(IDependencyContainer dependencyContainer, Type requiredType)
            {
                Guard.NotNull(dependencyContainer, "dependencyContainer");
                this.dependencyContainer = dependencyContainer;
                this.requiredType = requiredType;
            }

            public IDependencyTargetMapping In(DependencyLifetime lifetime)
            {
                this.lifetime = lifetime;
                return this;
            }

            public IDependencyContainer To(object target)
            {
                return dependencyContainer.AddMapping(requiredType, lifetime, target);
            }
        }

        #region 'Add' extensions

        public static IDependencyScopeMapping Add(this IDependencyContainer dependencyContainer, Type requiredType)
        {
            return new DependencyRegistration(dependencyContainer, requiredType);
        }

        public static IDependencyScopeMapping Add<TRequired>(this IDependencyContainer dependencyContainer)
        {
            return Add(dependencyContainer, typeof(TRequired));
        }

        #endregion

        #region 'In' extensions

        public static IDependencyTargetMapping InTransient(this IDependencyScopeMapping mapping)
        {
            Guard.NotNull(mapping, "mapping");
            return mapping.In(DependencyLifetime.Transient);
        }

        public static IDependencyTargetMapping InAnyScope(this IDependencyScopeMapping mapping)
        {
            Guard.NotNull(mapping, "mapping");
            return mapping.In(DependencyLifetime.AnyScope);
        }

        public static IDependencyTargetMapping InNamedScope(this IDependencyScopeMapping mapping, string scopeName)
        {
            Guard.NotNull(mapping, "mapping");
            return mapping.In(DependencyLifetime.NamedScope(scopeName));
        }

        #endregion

        #region 'To' extensions

        public static IDependencyContainer ToType(this IDependencyTargetMapping mapping, Type targetType)
        {
            Guard.NotNull(mapping, "mapping");
            return mapping.To(targetType);
        }

        public static IDependencyContainer ToActivator<TTarget>(this IDependencyTargetMapping mapping, IActivator<TTarget> activator)
        {
            Guard.NotNull(mapping, "mapping");
            return mapping.To(activator);
        }

        #endregion

        //public static 
    }
}
