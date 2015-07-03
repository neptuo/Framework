using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    public static class _DependencyContainerExtensions
    {
        #region NEW EXTENSIONS

        public static IDependencyContainer AddTransient<TImplementation>(this IDependencyContainer dependencyContainer)
        {
            Ensure.NotNull(dependencyContainer, "dependencyContainer");
            dependencyContainer.Definitions.Add(typeof(TImplementation), DependencyLifetime.Transient, typeof(TImplementation));
            return dependencyContainer;
        }

        public static IDependencyContainer AddTransient<T>(this IDependencyContainer dependencyContainer, T service)
        {
            Ensure.NotNull(dependencyContainer, "dependencyContainer");
            dependencyContainer.Definitions.Add(typeof(T), DependencyLifetime.Transient, service);
            return dependencyContainer;
        }

        public static IDependencyContainer AddTransient<TInterface, TImplementation>(this IDependencyContainer dependencyContainer)
        {
            Ensure.NotNull(dependencyContainer, "dependencyContainer");
            dependencyContainer.Definitions.Add(typeof(TInterface), DependencyLifetime.Transient, typeof(TImplementation));
            return dependencyContainer;
        }

        public static IDependencyContainer AddScoped<T>(this IDependencyContainer dependencyContainer, T service)
        {
            Ensure.NotNull(dependencyContainer, "dependencyContainer");
            dependencyContainer.Definitions.Add(typeof(T), DependencyLifetime.Transient, service);
            return dependencyContainer;
        }

        public static IDependencyContainer AddNameScoped<T>(this IDependencyContainer dependencyContainer, string scopeName, T service)
        {
            Ensure.NotNull(dependencyContainer, "dependencyContainer");
            dependencyContainer.Definitions.Add(typeof(T), DependencyLifetime.NameScoped(scopeName), service);
            return dependencyContainer;
        }

        public static IDependencyContainer AddNameScoped<T, TActivator>(this IDependencyContainer dependencyContainer, string scopeName, TActivator serviceActivator)
            where TActivator : IActivator<T>
        {
            Ensure.NotNull(dependencyContainer, "dependencyContainer");
            dependencyContainer.Definitions.Add(typeof(T), DependencyLifetime.NameScoped(scopeName), serviceActivator);
            return dependencyContainer;
        }

        public static IDependencyContainer AddCurrentScoped<T>(this IDependencyContainer dependencyContainer, T service)
        {
            Ensure.NotNull(dependencyContainer, "dependencyContainer");
            dependencyContainer.Definitions.Add(typeof(T), DependencyLifetime.NameScoped(null), service);
            return dependencyContainer;
        }

        #endregion



        #region Fluent interfaces and classes

        /// <summary>
        /// Provides ability to map <see cref="DependencyLifetime"/>.
        /// </summary>
        public interface IDependencyScopeMapping
        {
            /// <summary>
            /// Maps <see cref="DependencyLifetime"/> for <paramref name="model" />.
            /// </summary>
            /// <param name="lifetime">Chosen object life time.</param>
            /// <returns>Component for mapping target.</returns>
            IDependencyTargetMapping In(DependencyLifetime lifetime);

            /// <summary>
            /// Maps <paramref name="model" /> to have instance in scopes that are named same as the current scope 
            /// (= if any child scope will have different name, <paramref name="model" /> will produce singleton).
            /// </summary>
            /// <returns>Component for mapping target.</returns>
            IDependencyTargetMapping InCurrentScope();
        }

        /// <summary>
        /// Provides ability to map <paramref name="model" /> target.
        /// </summary>
        public interface IDependencyTargetMapping
        {
            /// <summary>
            /// Maps target of <paramref name="model" />.
            /// Can by any supported type, see container target feature configuration.
            /// </summary>
            /// <param name="target">Object to provide target.</param>
            /// <returns>Current cotainer to execute next actions.</returns>
            IDependencyContainer To(object target);

            /// <summary>
            /// Maps target of <paramref name="model" /> to self type (type set in the first step of the registration).
            /// </summary>
            /// <returns>Current cotainer to execute next actions.</returns>
            IDependencyContainer ToSelf();
        }

        /// <summary>
        /// Internal implementation of <see cref="IDependencyScopeMapping"/> and <see cref="IDependencyTargetMapping"/>.
        /// </summary>
        internal class DependencyRegistration : IDependencyScopeMapping, IDependencyTargetMapping
        {
            private readonly IDependencyContainer dependencyContainer;
            private readonly Type requiredType;
            private DependencyLifetime lifetime;

            public DependencyRegistration(IDependencyContainer dependencyContainer, Type requiredType)
            {
                Ensure.NotNull(dependencyContainer, "dependencyContainer");
                this.dependencyContainer = dependencyContainer;
                this.requiredType = requiredType;
            }

            public IDependencyTargetMapping In(DependencyLifetime lifetime)
            {
                this.lifetime = lifetime;
                return this;
            }
            public IDependencyTargetMapping InCurrentScope()
            {
                return In(DependencyLifetime.NameScoped(dependencyContainer.ScopeName));
            }

            public IDependencyContainer To(object target)
            {
                dependencyContainer.Definitions.Add(requiredType, lifetime, target);
                return dependencyContainer;
            }

            public IDependencyContainer ToSelf()
            {
                return To(requiredType);
            }
        }

        #endregion

        #region 'Map' extensions

        /// <summary>
        /// Starts fluent registration for <paramref name="requiredType"/>.
        /// </summary>
        /// <param name="dependencyContainer">Container to register type in.</param>
        /// <param name="requiredType">Source type for this registration.</param>
        /// <returns>Component for mapping object lifetime.</returns>
        public static IDependencyScopeMapping Map(this IDependencyContainer dependencyContainer, Type requiredType)
        {
            return new DependencyRegistration(dependencyContainer, requiredType);
        }

        /// <summary>
        /// Starts fluent registration for <typeparamref name="TRequired"/>
        /// </summary>
        /// <typeparam name="TRequired">Source type for this registration.</typeparam>
        /// <param name="dependencyContainer">Container to register type in.</param>
        /// <returns>Component for mapping object lifetime.</returns>
        public static IDependencyScopeMapping Map<TRequired>(this IDependencyContainer dependencyContainer)
        {
            return Map(dependencyContainer, typeof(TRequired));
        }

        #endregion

        #region 'In' extensions

        /// <summary>
        /// Maps <paramref name="model" /> to have transient life time (= new instance for every resolution).
        /// </summary>
        /// <param name="model">Current registration.</param>
        /// <returns>Component for mapping target.</returns>
        public static IDependencyTargetMapping InTransient(this IDependencyScopeMapping model)
        {
            Ensure.NotNull(model, "model");
            return model.In(DependencyLifetime.Transient);
        }

        /// <summary>
        /// Maps <paramref name="model" /> to have per scope life time (= new instance in every container scope).
        /// </summary>
        /// <param name="model">Current registration.</param>
        /// <returns>Component for mapping target.</returns>
        public static IDependencyTargetMapping InAnyScope(this IDependencyScopeMapping model)
        {
            Ensure.NotNull(model, "model");
            return model.In(DependencyLifetime.AnyScope);
        }

        /// <summary>
        /// Maps <paramref name="model" /> to have per concrete names scope life time (= new instance every scope named <paramref name="scopeName"/>).
        /// </summary>
        /// <param name="model">Current registration.</param>
        /// <param name="scopeName">Name of the scope.</param>
        /// <returns>Component for mapping target.</returns>
        public static IDependencyTargetMapping InNamedScope(this IDependencyScopeMapping model, string scopeName)
        {
            Ensure.NotNull(model, "model");
            return model.In(DependencyLifetime.NameScoped(scopeName));
        }

        #endregion

        #region 'To' extensions

        /// <summary>
        /// Maps <paramref name="model"/> to target type (= required type will be mapped to instance of <paramref name="targetType"/>).
        /// </summary>
        /// <param name="model">Current registration.</param>
        /// <param name="targetType">Target mapped type.</param>
        /// <returns>Current cotainer to execute next actions.</returns>
        public static IDependencyContainer ToType(this IDependencyTargetMapping model, Type targetType)
        {
            Ensure.NotNull(model, "model");
            return model.To(targetType);
        }

        /// <summary>
        /// Maps <paramref name="model"/> to target type (= required type will be mapped to instance of <typeparamref name="TTarget"/>).
        /// </summary>
        /// <typeparam name="TTarget">Target mapped type.</typeparam>
        /// <param name="model">Current registration.</param>
        /// <returns>Current cotainer to execute next actions.</returns>
        public static IDependencyContainer ToType<TTarget>(this IDependencyTargetMapping model)
        {
            return ToType(model, typeof(TTarget));
        }

        /// <summary>
        /// Maps <paramref name="model"/> to activator (= instance will be created using <paramref name="activator"/>).
        /// </summary>
        /// <typeparam name="TTarget">Type, that is provided by the <paramref name="activator"/>.</typeparam>
        /// <param name="model">Current registration.</param>
        /// <param name="activator">Activator to creates instances by, in this registration.</param>
        /// <returns>Current cotainer to execute next actions.</returns>
        public static IDependencyContainer ToActivator<TTarget>(this IDependencyTargetMapping model, IActivator<TTarget> activator)
        {
            Ensure.NotNull(model, "model");
            return model.To(activator);
        }

        #endregion
    }
}
