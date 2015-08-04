using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflection.Enumerators
{
    /// <summary>
    /// Default (enumeration) implementation of <see cref="ITypeEnumerator"/>.
    /// </summary>
    public class DefaultTypeEnumerator : ITypeEnumerator
    {
        private readonly IEnumerable<Type> types;
        private IEnumerator<Type> enumerator;

        public Type Current { get; private set; }

        /// <summary>
        /// Creates new instance that enumerates type from <paramref name="types"/>.
        /// </summary>
        /// <param name="types">Enumeration of types.</param>
        public DefaultTypeEnumerator(IEnumerable<Type> types)
        {
            Ensure.NotNull(types, "types");
            this.types = types;
        }

        public bool Next()
        {
            if (enumerator == null)
                enumerator = types.GetEnumerator();

            if(enumerator.MoveNext())
            {
                Current = enumerator.Current;
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }
    }
}
