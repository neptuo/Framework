using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Collections
{
    /// <summary>
    /// The collection of registered deserializers by <typeparamref name="TKey"/>.
    /// </summary>
    public interface IDeserializerCollection<TKey>
    {
        /// <summary>
        /// Adds <paramref name="key"/> to be serialized by <paramref name="deserializer"/>.
        /// </summary>
        /// <param name="key">The key to register <paramref name="deserializer"/> with.</param>
        /// <param name="deserializer">The deserializer to register.</param>
        /// <returns>Self (for fluency).</returns>
        IDeserializerCollection<TKey> Map(TKey key, IDeserializer deserializer);

        /// <summary>
        /// Tries to get deserializer registered with <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key to find deserializer registered with.</param>
        /// <param name="deserializer">The deserializer registered with <paramref name="key"/>.</param>
        /// <returns><c>true</c> if <paramref name="deserializer"/> was found; <c>false</c> otherwise.</returns>
        bool TryGet(TKey key, out IDeserializer deserializer);
    }
}
