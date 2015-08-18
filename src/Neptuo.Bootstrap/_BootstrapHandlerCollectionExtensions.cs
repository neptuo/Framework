using Neptuo.Activators;
using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    /// <summary>
    /// Common extensions for <see cref="IBootstrapHandlerCollection"/>.
    /// </summary>
    public static class _BootstrapHandlerCollectionExtensions
    {
        /// <summary>
        /// Adds <paramref name="handler"/> as instance to the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">Type of bootstrap handler.</typeparam>
        /// <param name="collection">Collection of handlers.</param>
        /// <param name="handler">Handler to be added.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IBootstrapHandlerCollection Add<T>(this IBootstrapHandlerCollection collection, T handler)
            where T : class, IBootstrapHandler
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(handler, "handler");
            return collection.Add(new InstanceFactory<T>(handler));
        }

        /// <summary>
        /// Adds <paramref name="handlerGetter"/> as provider for handler of type <typeparamref name="T"/> to the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">Type of bootstrap handler.</typeparam>
        /// <param name="collection">Collection of handlers.</param>
        /// <param name="handlerGetter">Func to provide instance of handler.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IBootstrapHandlerCollection Add<T>(this IBootstrapHandlerCollection collection, Func<T> handlerGetter)
            where T : class, IBootstrapHandler
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(handlerGetter, "handlerGetter");
            return collection.Add(new InstanceFactory<T>(handlerGetter));
        }

        /// <summary>
        /// Adds <typeparamref name="T"/> to be provided by <see cref="DependencyFactory{T}"/> to the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">Type of bootstrap handler.</typeparam>
        /// <param name="collection">Collection of handlers.</param>
        /// <param name="dependencyProvider">Provider used to create instance of <see cref="DependencyFactory{T}"/>.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IBootstrapHandlerCollection Add<T>(this IBootstrapHandlerCollection collection, IDependencyProvider dependencyProvider)
            where T : class, IBootstrapHandler
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(dependencyProvider, "dependencyProvider");
            return collection.Add(new DependencyFactory<T>(dependencyProvider));
        }

        /// <summary>
        /// Adds <typeparamref name="T"/> as defaulty created instance to the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">Type of bootstrap handler.</typeparam>
        /// <param name="collection">Collection of handlers.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IBootstrapHandlerCollection Add<T>(this IBootstrapHandlerCollection collection)
            where T : class, IBootstrapHandler, new()
        {
            Ensure.NotNull(collection, "collection");
            return collection.Add(new DefaultFactory<T>());
        }
    }
}
