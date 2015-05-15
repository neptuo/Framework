using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Serialization
{
    public class XmlTypeMappingCollection
    {
        private readonly Dictionary<string, Type> nameStorage = new Dictionary<string, Type>();
        private readonly Dictionary<Type, string> typeStorage = new Dictionary<Type, string>();
        private readonly OutFuncCollection<string, Type, bool> onSearchType = new OutFuncCollection<string, Type, bool>();
        private readonly OutFuncCollection<Type, string, bool> onSearchName = new OutFuncCollection<Type, string, bool>();

        public XmlTypeMappingCollection Add(string xmlName, Type mappedType)
        {
            Ensure.NotNullOrEmpty(xmlName, "xmlName");
            Ensure.NotNull(mappedType, "mappedType");
            nameStorage[xmlName] = mappedType;
            typeStorage[mappedType] = xmlName;
            return this;
        }

        public XmlTypeMappingCollection AddSearchTypeHandler(OutFunc<string, Type, bool> searchHandler)
        {
            onSearchType.Add(searchHandler);
            return this;
        }

        public XmlTypeMappingCollection AddSearchNameHandler(OutFunc<Type, string, bool> searchHandler)
        {
            onSearchName.Add(searchHandler);
            return this;
        }

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
