using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Generic
{
    /// <summary>
    /// A common extensions for <see cref="ICollection{T}"/>.
    /// </summary>
    public static class _CollectionExtensions
    {
        /// <summary>
        /// Adds all items from <paramref name="items"/> to a <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">A type of the item.</typeparam>
        /// <param name="collection">A target collection to add items to.</param>
        /// <param name="items">An enumeration of items to add to <paramref name="collection"/>.</param>
        /// <returns><paramref name="collection"/> with added items.</returns>
        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(items, "items");
            foreach (T item in items)
                collection.Add(item);

            return collection;
        }

        /// <summary>
        /// Adds all items from <paramref name="items"/> to a <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">A type of the item.</typeparam>
        /// <param name="collection">A target collection to add items to.</param>
        /// <param name="items">An enumeration of items to add to <paramref name="collection"/>.</param>
        /// <returns><paramref name="collection"/> with added items.</returns>
        public static ICollection<T> AddRange<T>(this ICollection<T> collection, params T[] items)
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(items, "items");
            foreach (T item in items)
                collection.Add(item);

            return collection;
        }
    }
}
