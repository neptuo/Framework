using Mirrored.SharpKit.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.SharpKit.Exugin.Exports
{
    public class TypeRegistry
    {
        private Dictionary<string, TypeRegistryItem> registy = new Dictionary<string, TypeRegistryItem>();

        public void Add(string typeName, TypeRegistryItem item)
        {
            if (typeName == null)
                throw new ArgumentNullException("typeName");

            if (item == null)
                throw new ArgumentNullException("item");

            registy[typeName] = item;
        }

        public void AddDefault(TypeRegistryItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            registy[String.Empty] = item;
        }

        public TypeRegistryItem Get(string typeName)
        {
            if (typeName == null)
                throw new ArgumentNullException("typeName");

            TypeRegistryItem item;
            if (registy.TryGetValue(typeName, out item))
                return item;

            if (registy.TryGetValue(String.Empty, out item))
                return item;

            return null;
        }
    }
}
