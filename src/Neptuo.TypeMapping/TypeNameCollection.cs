using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TypeMapping
{
    /// <summary>
    /// An implementation of <see cref="ITypeNameMapper"/> with manual registration.
    /// </summary>
    public class TypeNameCollection : ITypeNameMapper
    {
        private readonly object storageLock = new object();
        private readonly Dictionary<string, Type> stringToType = new Dictionary<string, Type>();
        private readonly Dictionary<Type, string> typeToString = new Dictionary<Type, string>();

        /// <summary>
        /// Adds mapping from <paramref name="typeName"/> to <paramref name="type"/> and vice-versa.
        /// </summary>
        /// <param name="typeName">A type string.</param>
        /// <param name="type">A clr type.</param>
        /// <returns>Self (for fluency).</returns>
        public TypeNameCollection AddDual(string typeName, Type type)
        {
            Ensure.NotNullOrEmpty(typeName, "typeName");
            Ensure.NotNull(type, "type");

            lock (storageLock)
            {
                stringToType[typeName] = type;
                typeToString[type] = typeName;
            }

            return this;
        }

        /// <summary>
        /// Adds mapping from <paramref name="typeName"/> to <paramref name="type"/>.
        /// </summary>
        /// <param name="typeName">A type string.</param>
        /// <param name="type">A clr type.</param>
        /// <returns>Self (for fluency).</returns>
        public TypeNameCollection Add(string typeName, Type type)
        {
            Ensure.NotNullOrEmpty(typeName, "typeName");
            Ensure.NotNull(type, "type");

            lock (storageLock)
                stringToType[typeName] = type;

            return this;
        }

        /// <summary>
        /// Adds mapping from <paramref name="type"/> to <paramref name="typeName"/>.
        /// </summary>
        /// <param name="type">A clr type.</param>
        /// <param name="typeName">A type string.</param>
        /// <returns>Self (for fluency).</returns>
        public TypeNameCollection Add(Type type, string typeName)
        {
            Ensure.NotNull(type, "type");
            Ensure.NotNullOrEmpty(typeName, "typeName");

            lock (storageLock)
                stringToType[typeName] = type;

            return this;
        }

        public bool TryGet(string typeName, out Type type)
        {
            Ensure.NotNullOrEmpty(typeName, "typeName");
            return stringToType.TryGetValue(typeName, out type);
        }

        public bool TryGet(Type type, out string typeName)
        {
            Ensure.NotNull(type, "type");
            return typeToString.TryGetValue(type, out typeName);
        }
    }
}
