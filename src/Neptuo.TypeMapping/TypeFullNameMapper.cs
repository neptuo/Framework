using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TypeMapping
{
    /// <summary>
    /// An implementation of <see cref="ITypeNameMapper"/> which uses only <see cref="Type.FullName"/> and lookups in passed assemblies.
    /// </summary>
    public class TypeFullNameMapper : ITypeNameMapper
    {
        private readonly List<Assembly> assemblies;

        /// <summary>
        /// Creates a new instance with <see cref="Assembly.GetCallingAssembly()"/>, <see cref="Assembly.GetExecutingAssembly()"/> and <see cref="Assembly.GetEntryAssembly()"/>.
        /// </summary>
        public TypeFullNameMapper()
            : this(Assembly.GetCallingAssembly(), Assembly.GetExecutingAssembly(), Assembly.GetEntryAssembly())
        { }

        /// <summary>
        /// Creates a new instance with <paramref name="assemblies"/>.
        /// </summary>
        /// <param name="assemblies">An enumeration array of assemblies to look for type in.</param>
        public TypeFullNameMapper(IEnumerable<Assembly> assemblies)
        {
            Ensure.NotNull(assemblies, "assemblies");
            this.assemblies = new List<Assembly>(assemblies);
            EnsureAssembliesCount();
        }

        /// <summary>
        /// Creates a new instance with <paramref name="assemblies"/>.
        /// </summary>
        /// <param name="assemblies">An array of assemblies to look for type in.</param>
        public TypeFullNameMapper(params Assembly[] assemblies)
        {
            Ensure.NotNull(assemblies, "assemblies");
            this.assemblies = new List<Assembly>(assemblies);
            EnsureAssembliesCount();
        }

        private void EnsureAssembliesCount()
        {
            if (assemblies.Count == 0)
                throw Ensure.Exception.ArgumentOutOfRange("assemblies", "There must be at least one assembly.");
        }

        public bool TryGet(string typeName, out Type type)
        {
            Ensure.NotNullOrEmpty(typeName, "typeName");

            foreach (Assembly assembly in assemblies)
            {
                type = assembly.GetType(typeName);
                if (type != null)
                    return true;
            }

            type = null;
            return false;
        }

        public bool TryGet(Type type, out string typeName)
        {
            Ensure.NotNull(type, "type");
            typeName = type.FullName;
            return true;
        }
    }
}
