using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// The extensions for converting <see cref="IKey"/>.
    /// </summary>
    public static class _KeyExtensions
    {
        /// <summary>
        /// Tries to convert/cast <paramref name="key"/> to the type <typeparamref name="TKey"/>.
        /// </summary>
        /// <typeparam name="TKey">The requested type of key.</typeparam>
        /// <param name="key">The key to convert/cast.</param>
        /// <returns>The converted/casted key.</returns>
        public static TKey As<TKey>(this IKey key)
            where TKey : class, IKey
        {
            TKey target = key as TKey;
            if(target == null)
                throw new NotImplementedException();

            return target;
        }
    }
}
