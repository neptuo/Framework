using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators
{
    /// <summary>
    /// Implementation of <see cref="ITypeEnumerator"/> that enumerates types from <see cref="Assembly"/>.
    /// </summary>
    public class AssemblyTypeEnumerator : DefaultTypeEnumerator
    {
        /// <summary>
        /// Creates new instance that enumerates types from <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly">Assembly to enumerate types from.</param>
        public AssemblyTypeEnumerator(Assembly assembly)
            : base(EnumerateTypes(assembly))
        { }

        private static IEnumerable<Type> EnumerateTypes(Assembly assembly)
        {
            Ensure.NotNull(assembly, "assembly");
            return assembly.GetTypes();
        }
    }
}
