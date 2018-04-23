using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Generic
{
    /// <summary>
    /// A generic extensions for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class _EnumerableExtensions
    {
        /// <summary>
        /// For each item in <paramref name="items"/> executes <paramref name="handler"/>.
        /// </summary>
        /// <typeparam name="T">A type of the item.</typeparam>
        /// <param name="items">An enumeration of items.</param>
        /// <param name="handler">A delegate to execute for each item.</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> handler)
        {
            foreach (T item in items)
                handler(item);
        }
    }
}
