using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Generic
{
    /// <summary>
    /// A generic extensions for <see cref="IDictionary{TKey, TValue}"/>
    /// </summary>
    public static class _DictionaryExtensions
    {
        /// <summary>
        /// Adds all <paramref name="source"/> to <paramref name="target"/>.
        /// </summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        /// <typeparam name="TValue">A type of the value.</typeparam>
        /// <param name="target">A collection to add items to.</param>
        /// <param name="source">A collection to add items from.</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> target, IDictionary<TKey, TValue> source)
        {
            Ensure.NotNull(target, "target");
            Ensure.NotNull(source, "source");
            foreach (KeyValuePair<TKey, TValue> item in source)
                target[item.Key] = item.Value;
        }
    }
}
