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
        IKeyToParametersConverter Add(IKeyValueCollection parameters, IKey key);
        IKeyToParametersConverter Add(IKeyValueCollection parameters, string prefix, IKey key);

        IKeyToParametersConverter AddWithoutType(IKeyValueCollection parameters, IKey key);
        IKeyToParametersConverter AddWithoutType(IKeyValueCollection parameters, string prefix, IKey key);

        bool TryGet<TKey>(IReadOnlyKeyValueCollection parameters, out TKey key) where TKey : IKey;
        bool TryGet<TKey>(IReadOnlyKeyValueCollection parameters, string prefix, out TKey key) where TKey : IKey;

        bool TryGetWithoutType<TKey>(IReadOnlyKeyValueCollection parameters, string keyType, out TKey key) where TKey : IKey;
        bool TryGetWithoutType<TKey>(IReadOnlyKeyValueCollection parameters, string keyType, string prefix, out TKey key) where TKey : IKey;

        bool TryGet(IReadOnlyKeyValueCollection parameters, out IKey key);
        bool TryGet(IReadOnlyKeyValueCollection parameters, string prefix, out IKey key);

        bool TryGetWithoutType(IReadOnlyKeyValueCollection parameters, string keyType, out IKey key);
        bool TryGetWithoutType(IReadOnlyKeyValueCollection parameters, string keyType, string prefix, out IKey key);
    }
}
