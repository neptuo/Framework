using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// A converter for serializing parameters to collection of parameters and back.
    /// </summary>
    public interface IKeyToParametersConverter
    {
        /// <summary>
        /// Adds <paramref name="key"/> to a collection of <paramref name="parameters"/>.
        /// Also, a type (<see cref="IKey.Type"/>) of the <paramref name="key"/> will be added.
        /// </summary>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="key">A key to add.</param>
        /// <returns>Self (for fluency).</returns>
        IKeyToParametersConverter Add(IKeyValueCollection parameters, IKey key);

        /// <summary>
        /// Adds <paramref name="key"/> to a collection of <paramref name="parameters"/> 
        /// where all added keys will be prefixed by <paramref name="prefix"/>.
        /// Also, a type (<see cref="IKey.Type"/>) of the <paramref name="key"/> will be added.
        /// </summary>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="prefix">A prefix to be added to all added keys.</param>
        /// <param name="key">A key to add.</param>
        /// <returns>Self (for fluency).</returns>
        IKeyToParametersConverter Add(IKeyValueCollection parameters, string prefix, IKey key);

        /// <summary>
        /// Adds <paramref name="key"/> to a collection of <paramref name="parameters"/> 
        /// and <see cref="IKey.Type"/> will be skipped.
        /// Also, a type (<see cref="IKey.Type"/>) of the <paramref name="key"/> will be added.
        /// </summary>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="key">A key to add.</param>
        /// <returns>Self (for fluency).</returns>
        IKeyToParametersConverter AddWithoutType(IKeyValueCollection parameters, IKey key);

        /// <summary>
        /// Adds <paramref name="key"/> to a collection of <paramref name="parameters"/> 
        /// where all added keys will be prefixed by <paramref name="prefix"/> 
        /// and <see cref="IKey.Type"/> will be skipped.
        /// Also, a type (<see cref="IKey.Type"/>) of the <paramref name="key"/> will be added.
        /// </summary>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="prefix">A prefix to be added to all added keys.</param>
        /// <param name="key">A key to add.</param>
        /// <returns>Self (for fluency).</returns>
        IKeyToParametersConverter AddWithoutType(IKeyValueCollection parameters, string prefix, IKey key);


        /// <summary>
        /// Tries to get s <paramref name="key"/> from <paramref name="parameters"/>.
        /// </summary>
        /// <typeparam name="TKey">A type of the to get.</typeparam>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="key">A created key or <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="key"/> was created; <c>false</c> otherwise</returns>
        bool TryGet<TKey>(IReadOnlyKeyValueCollection parameters, out TKey key) 
            where TKey : IKey;

        /// <summary>
        /// Tries to get s <paramref name="key"/> from <paramref name="parameters"/> 
        /// where all keys to read will be prefixed by <paramref name="prefix"/>.
        /// </summary>
        /// <typeparam name="TKey">A type of the to get.</typeparam>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="prefix">A prefix to be when reading all kes from <paramref name="parameters"/>.</param>
        /// <param name="key">A created key or <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="key"/> was created; <c>false</c> otherwise</returns>
        bool TryGet<TKey>(IReadOnlyKeyValueCollection parameters, string prefix, out TKey key) 
            where TKey : IKey;

        /// <summary>
        /// Tries to get s <paramref name="key"/> from <paramref name="parameters"/> 
        /// and <see cref="IKey.Type"/> will be set to <paramref name="keyType"/>.
        /// </summary>
        /// <typeparam name="TKey">A type of the to get.</typeparam>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="keyType">A <see cref="IKey.Type"/> to use.</param>
        /// <param name="key">A created key or <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="key"/> was created; <c>false</c> otherwise</returns>
        bool TryGetWithoutType<TKey>(IReadOnlyKeyValueCollection parameters, string keyType, out TKey key) 
            where TKey : IKey;

        /// <summary>
        /// Tries to get s <paramref name="key"/> from <paramref name="parameters"/> 
        /// where all keys to read will be prefixed by <paramref name="prefix"/> 
        /// and <see cref="IKey.Type"/> will be set to <paramref name="keyType"/>.
        /// </summary>
        /// <typeparam name="TKey">A type of the to get.</typeparam>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="keyType">A <see cref="IKey.Type"/> to use.</param>
        /// <param name="prefix">A prefix to be when reading all kes from <paramref name="parameters"/>.</param>
        /// <param name="key">A created key or <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="key"/> was created; <c>false</c> otherwise</returns>
        bool TryGetWithoutType<TKey>(IReadOnlyKeyValueCollection parameters, string keyType, string prefix, out TKey key) 
            where TKey : IKey;


        /// <summary>
        /// Tries to get s <paramref name="key"/> from <paramref name="parameters"/>.
        /// Concrete type of the <see cref="IKey"/> will be determined based on registration.
        /// </summary>
        /// <typeparam name="TKey">A type of the to get.</typeparam>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="key">A created key or <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="key"/> was created; <c>false</c> otherwise</returns>
        bool TryGet(IReadOnlyKeyValueCollection parameters, out IKey key);

        /// <summary>
        /// Tries to get s <paramref name="key"/> from <paramref name="parameters"/> 
        /// where all keys to read will be prefixed by <paramref name="prefix"/>.
        /// 
        /// Concrete type of the <see cref="IKey"/> will be determined based on registration.
        /// </summary>
        /// <typeparam name="TKey">A type of the to get.</typeparam>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="prefix">A prefix to be when reading all kes from <paramref name="parameters"/>.</param>
        /// <param name="key">A created key or <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="key"/> was created; <c>false</c> otherwise</returns>
        bool TryGet(IReadOnlyKeyValueCollection parameters, string prefix, out IKey key);

        /// <summary>
        /// Tries to get s <paramref name="key"/> from <paramref name="parameters"/> 
        /// and <see cref="IKey.Type"/> will be set to <paramref name="keyType"/>.
        /// 
        /// Concrete type of the <see cref="IKey"/> will be determined based on registration.
        /// </summary>
        /// <typeparam name="TKey">A type of the to get.</typeparam>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="keyType">A <see cref="IKey.Type"/> to use.</param>
        /// <param name="key">A created key or <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="key"/> was created; <c>false</c> otherwise</returns>
        bool TryGetWithoutType(IReadOnlyKeyValueCollection parameters, string keyType, out IKey key);

        /// <summary>
        /// Tries to get s <paramref name="key"/> from <paramref name="parameters"/> 
        /// where all keys to read will be prefixed by <paramref name="prefix"/> 
        /// and <see cref="IKey.Type"/> will be set to <paramref name="keyType"/>.
        /// 
        /// Concrete type of the <see cref="IKey"/> will be determined based on registration.
        /// </summary>
        /// <typeparam name="TKey">A type of the to get.</typeparam>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="keyType">A <see cref="IKey.Type"/> to use.</param>
        /// <param name="prefix">A prefix to be when reading all kes from <paramref name="parameters"/>.</param>
        /// <param name="key">A created key or <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="key"/> was created; <c>false</c> otherwise</returns>
        bool TryGetWithoutType(IReadOnlyKeyValueCollection parameters, string keyType, string prefix, out IKey key);
    }
}
