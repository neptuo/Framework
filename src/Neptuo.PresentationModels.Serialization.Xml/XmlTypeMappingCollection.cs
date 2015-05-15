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
        private readonly Dictionary<string, Type> storage = new Dictionary<string, Type>();
        private readonly OutFuncCollection<string, Type, bool> onSearchType = new OutFuncCollection<string, Type, bool>();

        public XmlTypeMappingCollection Add(string typeName, Type targetType)
        {
            Ensure.NotNullOrEmpty(typeName, "typeName");
            Ensure.NotNull(targetType, "targetType");
            storage[typeName] = targetType;
            return this;
        }

        public bool TryGet(string typeName, out Type targetType)
        {
            Ensure.NotNullOrEmpty(typeName, "typeName");

            if (storage.TryGetValue(typeName, out targetType))
                return true;

            if (onSearchType.TryExecute(typeName, out targetType))
                return true;

            targetType = null;
            return false;
        }
    }
}
