using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Storages
{
    /// <summary>
    /// The formatter used to write values to the <see cref="ICompositeStorage"/> from <see cref="CompositeFormatter"/>.
    /// </summary>
    public interface ICompositeStorageFormatter
    {
        /// <summary>
        /// Tries to read value of type <paramref name="valueType"/> with <paramref name="key"/> from <paramref name="storage"/>.
        /// </summary>
        /// <param name="storage">The storage with serialized values.</param>
        /// <param name="key">The key to load.</param>
        /// <param name="valueType">The type of the value to load.</param>
        /// <param name="value">The loaded value or <c>null</c>.</param>
        /// <returns><c>true</c> if loading value of type <paramref name="value"/> was successfull; <c>false</c> otherwise.</returns>
        bool TryDeserialize(ICompositeStorage storage, string key, Type valueType, out object value);

        /// <summary>
        /// Tries to storage <paramref name="value"/> to the <paramref name="storage"/> with <paramref name="key"/>.
        /// </summary>
        /// <param name="storage">The storage to write to.</param>
        /// <param name="key">The key to store.</param>
        /// <param name="value">The value to store.</param>
        /// <returns><c>true</c> if <paramref name="value"/> was successfully stored; <c>false</c> otherwise.</returns>
        bool TrySerialize(ICompositeStorage storage, string key, object value);
    }
}
