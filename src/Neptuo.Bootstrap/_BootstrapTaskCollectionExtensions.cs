using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    /// <summary>
    /// Common extensions for <see cref="IBootstrapTaskCollection"/>.
    /// </summary>
    public static class _BootstrapTaskCollectionExtensions
    {
        /// <summary>
        /// Adds <paramref name="task"/> as instance to the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">Type of bootstrap task.</typeparam>
        /// <param name="collection">Collection of tasks.</param>
        /// <param name="task">Task to be added.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IBootstrapTaskCollection Add<T>(this IBootstrapTaskCollection collection, T task)
            where T : class, IBootstrapTask
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(task, "task");
            return collection.Add(new InstanceFactory<T>(task));
        }

        /// <summary>
        /// Adds <paramref name="taskGetter"/> as provider for task of type <typeparamref name="T"/> to the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">Type of bootstrap task.</typeparam>
        /// <param name="collection">Collection of tasks.</param>
        /// <param name="taskGetter">Func to provide instance of task.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IBootstrapTaskCollection Add<T>(this IBootstrapTaskCollection collection, Func<T> taskGetter)
            where T : class, IBootstrapTask
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(taskGetter, "taskGetter");
            return collection.Add(new InstanceFactory<T>(taskGetter));
        }

        /// <summary>
        /// Adds <typeparamref name="T"/> to be provided by <see cref="DependencyFactory{T}"/> to the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">Type of bootstrap task.</typeparam>
        /// <param name="collection">Collection of tasks.</param>
        /// <param name="dependencyProvider">Provider used to create instance of <see cref="DependencyFactory{T}"/>.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IBootstrapTaskCollection Add<T>(this IBootstrapTaskCollection collection, IDependencyProvider dependencyProvider)
            where T : class, IBootstrapTask
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(dependencyProvider, "dependencyProvider");
            return collection.Add(new DependencyFactory<T>(dependencyProvider));
        }

        /// <summary>
        /// Adds <typeparamref name="T"/> as defaulty created instance to the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">Type of bootstrap task.</typeparam>
        /// <param name="collection">Collection of tasks.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static IBootstrapTaskCollection Add<T>(this IBootstrapTaskCollection collection)
            where T : class, IBootstrapTask, new()
        {
            Ensure.NotNull(collection, "collection");
            return collection.Add(new DefaultFactory<T>());
        }
    }
}
