using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// An implementation of <see cref="IKeyTypeProvider"/> which uses only <see cref="Type.FullName"/> and lookups in passed single assembly.
    /// </summary>
    public class TypeFullNameKeyTypeProvider : IKeyTypeProvider
    {
        private readonly List<Assembly> assemblies;

        /// <summary>
        /// Creates a new instance with <see cref="Assembly.GetCallingAssembly()"/>, <see cref="Assembly.GetExecutingAssembly()"/> and <see cref="Assembly.GetEntryAssembly()"/>.
        /// </summary>
        public TypeFullNameKeyTypeProvider()
            : this(Assembly.GetCallingAssembly(), Assembly.GetExecutingAssembly(), Assembly.GetEntryAssembly())
        { }

        /// <summary>
        /// Creates a new instance with <paramref name="assemblies"/>.
        /// </summary>
        /// <param name="assemblies">An enumeration array of assemblies to look for type in.</param>
        public TypeFullNameKeyTypeProvider(IEnumerable<Assembly> assemblies)
        {
            Ensure.NotNull(assemblies, "assemblies");
            this.assemblies = new List<Assembly>(assemblies);
            EnsureAssembliesCount();
        }

        /// <summary>
        /// Creates a new instance with <paramref name="assemblies"/>.
        /// </summary>
        /// <param name="assemblies">An array of assemblies to look for type in.</param>
        public TypeFullNameKeyTypeProvider(params Assembly[] assemblies)
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

        public bool TryGet(string keyType, out Type type)
        {
            Ensure.NotNullOrEmpty(keyType, "keyType");

            foreach (Assembly assembly in assemblies)
            {
                type = assembly.GetType(keyType);
                if (type != null)
                    return true;
            }

            type = null;
            return false;
        }

        public bool TryGet(Type type, out string keyType)
        {
            Ensure.NotNull(type, "type");
            keyType = type.FullName;
            return true;
        }
    }
}
