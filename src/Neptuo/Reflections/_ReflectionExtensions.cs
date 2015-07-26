using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections
{
    /// <summary>
    /// Common extensions for <see cref="System.Reflection"/>.
    /// </summary>
    public static class _ReflectionExtensions
    {
        /// <summary>
        /// Returns <c>true</c> when <paramref name="type"/> has default (parameter-less) constructor; <c>false</c> otherwise.
        /// </summary>
        /// <param name="type">Type to test.</param>
        /// <returns><c>true</c> when <paramref name="type"/> has default (parameter-less) constructor; <c>false</c> otherwise.</returns>
        public static bool HasDefaultConstructor(this Type type)
        {
            Ensure.NotNull(type, "type");
            return type.GetConstructor(new Type[0]) != null;
        }
    }
}
