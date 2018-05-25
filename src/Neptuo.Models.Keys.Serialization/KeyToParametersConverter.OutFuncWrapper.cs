using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    partial class KeyToParametersConverter
    {
        /// <summary>
        /// Wraps generic outfunc to non-generic.
        /// </summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        private class OutFuncWrapper<TKey>
            where TKey : IKey
        {
            private readonly OutFunc<IReadOnlyKeyValueCollection, TKey, bool> inner;

            public OutFuncWrapper(OutFunc<IReadOnlyKeyValueCollection, TKey, bool> inner)
            {
                Ensure.NotNull(inner, "inner");
                this.inner = inner;
            }

            public bool TryGet(IReadOnlyKeyValueCollection parameters, out IKey key)
            {
                TKey rawKey;
                if (inner(parameters, out rawKey))
                {
                    key = rawKey;
                    return true;
                }

                key = null;
                return false;
            }
        }
    }
}
