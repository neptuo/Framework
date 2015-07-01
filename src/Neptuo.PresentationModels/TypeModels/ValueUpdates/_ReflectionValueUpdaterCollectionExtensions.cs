using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.ValueUpdates
{
    /// <summary>
    /// Common extensions for <see cref="ReflectionValueUpdaterCollection"/>.
    /// </summary>
    public static class _ReflectionValueUpdaterCollectionExtensions
    {
        /// <summary>
        /// Adds mapping of property of type <typeparamref name="T"/> to <paramref name="updater"/>.
        /// </summary>
        /// <typeparam name="T">Type of property to be updated by <paramref name="updater"/>.</typeparam>
        /// <param name="collection">Target updaters collection.</param>
        /// <param name="updater">Updater to be used at properties of type <typeparamref name="T"/>.</param>
        /// <returns>Self (for fluency).</returns>
        public static ReflectionValueUpdaterCollection Add<T>(this ReflectionValueUpdaterCollection collection, IReflectionValueUpdater updater)
        {
            Ensure.NotNull(collection, "collection");
            return collection.Add(typeof(T), updater);
        }
    }
}
