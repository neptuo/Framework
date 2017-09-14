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
