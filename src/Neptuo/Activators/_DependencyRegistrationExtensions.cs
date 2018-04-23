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

        /// <summary>
        /// Maps <typeparamref name="TInterface"/> to <typeparamref name="TImplementation"/> in transient scope.
        /// </summary>
        /// <typeparam name="TInterface">A type of the contract.</typeparam>
        /// <typeparam name="TImplementation">A type of the implementation.</typeparam>
        /// <param name="collection">A collection of dependencies.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddTransient<TInterface, TImplementation>(this IDependencyDefinitionCollection collection)
            where TImplementation : TInterface
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.Transient, typeof(TImplementation));
            return collection;
        }

        /// <summary>
        /// Registers <typeparamref name="TImplementation"/> as a transient service.
        /// </summary>
        /// <typeparam name="TImplementation">A type of the service.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddTransient<TImplementation>(this IDependencyDefinitionCollection collection)
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TImplementation), DependencyLifetime.Transient, typeof(TImplementation));
            return collection;
        }

        /// <summary>
        /// Registers <paramref name="factory"/> to be factory for transient services of type <typeparamref name="TInterface"/>.
        /// </summary>
        /// <typeparam name="TInterface">A type of the contract.</typeparam>
        /// <typeparam name="TFactory">A type of the factory.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <param name="factory">An instance of factory for services of type <typeparamref name="TInterface"/>.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddTransientFactory<TInterface, TFactory>(this IDependencyDefinitionCollection collection, TFactory factory)
            where TFactory : IFactory<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.Transient, factory);
            return collection;
        }

        /// <summary>
        /// Registers <typeparamref name="TFactory"/> to be factory for transient services of type <typeparamref name="TInterface"/>.
        /// </summary>
        /// <typeparam name="TInterface">A type of the contract.</typeparam>
        /// <typeparam name="TFactory">A type of the factory.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddTransientFactory<TInterface, TFactory>(this IDependencyDefinitionCollection collection)
            where TFactory : IFactory<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.Transient, typeof(TFactory));
            return collection;
        }

        #endregion

        #region AddScoped

        /// <summary>
        /// Maps <typeparamref name="TInterface"/> to <typeparamref name="TImplementation"/> in any scope.
        /// </summary>
        /// <typeparam name="TInterface">A type of the contract.</typeparam>
        /// <typeparam name="TImplementation">A type of the implementation.</typeparam>
        /// <param name="collection">A collection of dependencies.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddScoped<TInterface, TImplementation>(this IDependencyDefinitionCollection collection)
            where TImplementation : TInterface
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.Scope, typeof(TImplementation));
            return collection;
        }

        /// <summary>
        /// Registers <typeparamref name="TImplementation"/> as a service with single instance for every scope.
        /// </summary>
        /// <typeparam name="TImplementation">A type of the service.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddScoped<TImplementation>(this IDependencyDefinitionCollection collection)
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TImplementation), DependencyLifetime.Scope, typeof(TImplementation));
            return collection;
        }

        /// <summary>
        /// Registers <paramref name="factory"/> to be factory for services of type <typeparamref name="TInterface"/> with single instance for every scope.
        /// </summary>
        /// <typeparam name="TInterface">A type of the contract.</typeparam>
        /// <typeparam name="TFactory">A type of the factory.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <param name="factory">An instance of factory for services of type <typeparamref name="TInterface"/>.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddScopedFactory<TInterface, TFactory>(this IDependencyDefinitionCollection collection, TFactory factory)
            where TFactory : IFactory<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.Scope, factory);
            return collection;
        }

        /// <summary>
        /// Registers <typeparamref name="TFactory"/> to be factory for services of type <typeparamref name="TInterface"/> with single instance for every scope.
        /// </summary>
        /// <typeparam name="TInterface">A type of the contract.</typeparam>
        /// <typeparam name="TFactory">A type of the factory.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddScopedFactory<TInterface, TFactory>(this IDependencyDefinitionCollection collection)
            where TFactory : IFactory<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.Scope, typeof(TFactory));
            return collection;
        }

        #endregion

        #region AddScoped

        /// <summary>
        /// Maps <typeparamref name="TInterface"/> to <typeparamref name="TImplementation"/> in scope named <paramref name="scopeName"/>.
        /// </summary>
        /// <typeparam name="TInterface">A type of the contract.</typeparam>
        /// <typeparam name="TImplementation">A type of the implementation.</typeparam>
        /// <param name="collection">A collection of dependencies.</param>
        /// <param name="scopeName">A name of the scope where service will be available.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddScoped<TInterface, TImplementation>(this IDependencyDefinitionCollection collection, string scopeName)
            where TImplementation : TInterface
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.NameScope(scopeName), typeof(TImplementation));
            return collection;
        }

        /// <summary>
        /// Registers <typeparamref name="TImplementation"/> as a singleton service in scope named <paramref name="scopeName"/>.
        /// </summary>
        /// <typeparam name="TImplementation">A type of the service.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <param name="scopeName">A na of the scope where service will be available.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddScoped<TImplementation>(this IDependencyDefinitionCollection collection, string scopeName)
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TImplementation), DependencyLifetime.NameScope(scopeName), typeof(TImplementation));
            return collection;
        }

        /// <summary>
        /// Registers <typeparamref name="TImplementation"/> as a singleton <paramref name="instance"/> in scope named <paramref name="scopeName"/>.
        /// </summary>
        /// <typeparam name="TImplementation">A type of the service.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <param name="scopeName">A na of the scope where service will be available.</param>
        /// <param name="instance">An instance to available in scope named <paramref name="scopeName"/>.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddScoped<TImplementation>(this IDependencyDefinitionCollection collection, string scopeName, TImplementation instance)
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TImplementation), DependencyLifetime.NameScope(scopeName), instance);
            return collection;
        }

        /// <summary>
        /// Registers <paramref name="factory"/> to be factory for single  service of type <typeparamref name="TInterface"/> in scope named <paramref name="scopeName"/>.
        /// </summary>
        /// <typeparam name="TInterface">A type of the contract.</typeparam>
        /// <typeparam name="TFactory">A type of the factory.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <param name="scopeName">A na of the scope where service will be available.</param>
        /// <param name="factory">An instance of factory for services of type <typeparamref name="TInterface"/>.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddScopedFactory<TInterface, TFactory>(this IDependencyDefinitionCollection collection, string scopeName, TFactory factory)
            where TFactory : IFactory<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.NameScope(scopeName), factory);
            return collection;
        }

        /// <summary>
        /// Registers<typeparamref name="TFactory"/> to be factory for single  service of type <typeparamref name="TInterface"/> in scope named <paramref name="scopeName"/>.
        /// </summary>
        /// <typeparam name="TInterface">A type of the contract.</typeparam>
        /// <typeparam name="TFactory">A type of the factory.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <param name="scopeName">A na of the scope where service will be available.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddScopedFactory<TInterface, TFactory>(this IDependencyDefinitionCollection collection, string scopeName)
            where TFactory : IFactory<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.NameScope(scopeName), typeof(TFactory));
            return collection;
        }

        #endregion

        #region AddSingleton

        /// <summary>
        /// Maps <typeparamref name="TInterface"/> to <typeparamref name="TImplementation"/> in current scope (as a singleton).
        /// </summary>
        /// <typeparam name="TInterface">A type of the contract.</typeparam>
        /// <typeparam name="TImplementation">A type of the implementation.</typeparam>
        /// <param name="collection">A collection of dependencies.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddSingleton<TInterface, TImplementation>(this IDependencyDefinitionCollection collection)
            where TImplementation : TInterface
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.NameScope(collection.ScopeName), typeof(TImplementation));
            return collection;
        }

        /// <summary>
        /// Registers <typeparamref name="TImplementation"/> as a singleton service in current scope.
        /// </summary>
        /// <typeparam name="TImplementation">A type of the service.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddSingleton<TImplementation>(this IDependencyDefinitionCollection collection)
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TImplementation), DependencyLifetime.NameScope(collection.ScopeName), typeof(TImplementation));
            return collection;
        }

        /// <summary>
        /// Registers <typeparamref name="TImplementation"/> as a singleton <paramref name="instance"/> in current scope.
        /// </summary>
        /// <typeparam name="TImplementation">A type of the service.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <param name="instance">An instance to be used.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddSingleton<TImplementation>(this IDependencyDefinitionCollection collection, TImplementation instance)
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TImplementation), DependencyLifetime.NameScope(collection.ScopeName), instance);
            return collection;
        }

        /// <summary>
        /// Registers <paramref name="factory"/> to be factory for singleton service of type <typeparamref name="TInterface"/>.
        /// </summary>
        /// <typeparam name="TInterface">A type of the contract.</typeparam>
        /// <typeparam name="TFactory">A type of the factory.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <param name="factory">An instance of factory for singleton service of type <typeparamref name="TInterface"/>.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddSingletonFactory<TInterface, TFactory>(this IDependencyDefinitionCollection collection, TFactory factory)
            where TFactory : IFactory<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.NameScope(collection.ScopeName), factory);
            return collection;
        }

        /// <summary>
        /// Registers <typeparamref name="TFactory"/> to be factory for singleton service of type <typeparamref name="TInterface"/>.
        /// </summary>
        /// <typeparam name="TInterface">A type of the contract.</typeparam>
        /// <typeparam name="TFactory">A type of the factory.</typeparam>
        /// <param name="collection">A collection dependencies.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IDependencyDefinitionCollection AddSingletonFactory<TInterface, TFactory>(this IDependencyDefinitionCollection collection)
            where TFactory : IFactory<TInterface>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(typeof(TInterface), DependencyLifetime.NameScope(collection.ScopeName), typeof(TFactory));
            return collection;
        }

        #endregion
    }
}
