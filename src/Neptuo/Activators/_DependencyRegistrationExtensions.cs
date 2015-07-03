using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Common extensions for registering type to <see cref="IDependencyDefinitionCollection"/>.
    /// </summary>
    public static class _DependencyRegistrationExtensions
    {
        #region AddTransient

        public static IDependencyDefinitionCollection AddTransient<TInterface, TImplementation>(this IDependencyDefinitionCollection collection)
            where TInterface : TImplementation
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.Transient, typeof(TImplementation));
            return collection;
        }

        public static IDependencyDefinitionCollection AddTransient<TImplementation>(this IDependencyDefinitionCollection collection)
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TImplementation), DependencyLifetime.Transient, typeof(TImplementation));
            return collection;
        }

        public static IDependencyDefinitionCollection AddTransientActivator<TInterface, TActivator>(this IDependencyDefinitionCollection collection, TActivator activator)
            where TActivator : IActivator<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.Transient, activator);
            return collection;
        }

        public static IDependencyDefinitionCollection AddTransientActivator<TInterface, TActivator>(this IDependencyDefinitionCollection collection)
            where TActivator : IActivator<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.Transient, typeof(TActivator));
            return collection;
        }

        #endregion

        #region AddScoped

        public static IDependencyDefinitionCollection AddScoped<TInterface, TImplementation>(this IDependencyDefinitionCollection collection)
            where TInterface : TImplementation
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.Scope, typeof(TImplementation));
            return collection;
        }

        public static IDependencyDefinitionCollection AddScoped<TImplementation>(this IDependencyDefinitionCollection collection)
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TImplementation), DependencyLifetime.Scope, typeof(TImplementation));
            return collection;
        }

        public static IDependencyDefinitionCollection AddScopedActivator<TInterface, TActivator>(this IDependencyDefinitionCollection collection, TActivator activator)
            where TActivator : IActivator<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.Scope, activator);
            return collection;
        }

        public static IDependencyDefinitionCollection AddScopedActivator<TInterface, TActivator>(this IDependencyDefinitionCollection collection)
            where TActivator : IActivator<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.Scope, typeof(TActivator));
            return collection;
        }

        #endregion

        #region AddNameScoped

        public static IDependencyDefinitionCollection AddNameScoped<TInterface, TImplementation>(this IDependencyDefinitionCollection collection, string scopeName)
            where TInterface : TImplementation
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.NameScope(scopeName), typeof(TImplementation));
            return collection;
        }

        public static IDependencyDefinitionCollection AddNameScoped<TImplementation>(this IDependencyDefinitionCollection collection, string scopeName)
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TImplementation), DependencyLifetime.NameScope(scopeName), typeof(TImplementation));
            return collection;
        }

        public static IDependencyDefinitionCollection AddNameScoped<TImplementation>(this IDependencyDefinitionCollection collection, string scopeName, TImplementation instance)
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TImplementation), DependencyLifetime.NameScope(scopeName), instance);
            return collection;
        }

        public static IDependencyDefinitionCollection AddNameScopedActivator<TInterface, TActivator>(this IDependencyDefinitionCollection collection, string scopeName, TActivator activator)
            where TActivator : IActivator<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.NameScope(scopeName), activator);
            return collection;
        }

        public static IDependencyDefinitionCollection AddNameScopedActivator<TInterface, TActivator>(this IDependencyDefinitionCollection collection, string scopeName)
            where TActivator : IActivator<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.NameScope(scopeName), typeof(TActivator));
            return collection;
        }

        #endregion
    }
}
