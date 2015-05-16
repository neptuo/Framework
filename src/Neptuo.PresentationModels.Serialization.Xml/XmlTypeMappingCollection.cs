using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// Collection for mapping XML type names to the .NET type system and vice versa.
    /// </summary>
    public class XmlTypeMappingCollection
    {
        private readonly Dictionary<string, Type> nameStorage = new Dictionary<string, Type>();
        private readonly Dictionary<Type, string> typeStorage = new Dictionary<Type, string>();
        private readonly OutFuncCollection<string, Type, bool> onSearchType = new OutFuncCollection<string, Type, bool>();
        private readonly OutFuncCollection<Type, string, bool> onSearchName = new OutFuncCollection<Type, string, bool>();

        /// <summary>
        /// Maps XML name <paramref name="xmlName"/> to the .NET type <paramref name="mappedType"/> and vice versa.
        /// </summary>
        /// <param name="xmlName">XML type name.</param>
        /// <param name="mappedType">.NET type.</param>
        /// <returns>Self (for fluency).</returns>
        public XmlTypeMappingCollection Add(string xmlName, Type mappedType)
        {
            Ensure.NotNullOrEmpty(xmlName, "xmlName");
            Ensure.NotNull(mappedType, "mappedType");
            nameStorage[xmlName] = mappedType;
            typeStorage[mappedType] = xmlName;
            return this;
        }

        /// <summary>
        /// Adds handle to be executed when coverting XML type name to .NET type and type mapping is missing.
        /// </summary>
        /// <param name="searchHandler">Handle to find type mapping.</param>
        /// <returns>Self (for fluency).</returns>
        public XmlTypeMappingCollection AddSearchTypeHandler(OutFunc<string, Type, bool> searchHandler)
        {
            onSearchType.Add(searchHandler);
            return this;
        }

        /// <summary>
        /// Adds handle to be executed when coverting .NET type to XML type name and type mapping is missing.
        /// </summary>
        /// <param name="searchHandler">Handle to find type mapping.</param>
        /// <returns>Self (for fluency).</returns>
        public XmlTypeMappingCollection AddSearchNameHandler(OutFunc<Type, string, bool> searchHandler)
        {
            onSearchName.Add(searchHandler);
            return this;
        }

        /// <summary>
        /// Tries to find mapping from <paramref name="typeName"/> to <paramref name="targetType"/>
        /// when converting XML type name to .NET type.
        /// </summary>
        /// <param name="typeName">XML type name.</param>
        /// <param name="targetType">Mapped .NET type.</param>
        /// <returns><c>true</c> when mapping does exist; <c>false</c> otherwise.</returns>
        public bool TryGetMappedType(string typeName, out Type targetType)
        {
            Ensure.NotNullOrEmpty(typeName, "typeName");

            if (nameStorage.TryGetValue(typeName, out targetType))
                return true;

            if (onSearchType.TryExecute(typeName, out targetType))
                return true;

            targetType = null;
            return false;
        }

        /// <summary>
        /// Tries to find mapping from <paramref name="typeName"/> to <paramref name="targetType"/>
        /// when converting .NET type to XML type name.
        /// </summary>
        /// <param name="type">.NET type.</param>
        /// <param name="targetTypeName">Mapped XML type name.</param>
        /// <returns><c>true</c> when mapping does exist; <c>false</c> otherwise.</returns>
        public bool TryGetXmlName(Type type, out string targetTypeName)
        {
            Ensure.NotNull(type, "type");

            if (typeStorage.TryGetValue(type, out targetTypeName))
                return true;

            if (onSearchName.TryExecute(type, out targetTypeName))
                return true;

            targetTypeName = null;
            return false;
        }
    }
}
