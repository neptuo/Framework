using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TypeMapping
{
    /// <summary>
    /// An implementation of <see cref="ITypeNameMapper"/> which uses <see cref="Type.FullName"/> and <see cref="Type.Assembly.GetName().Name"/>, separated by comma and space.
    /// </summary>
    public class TypeFullNameWithAssemblyMapper : ITypeNameMapper
    {
        public bool TryGet(string typeName, out Type type)
        {
            Ensure.NotNullOrEmpty(typeName, "typeName");
            type = Type.GetType(typeName);
            return type != null;
        }

        public bool TryGet(Type type, out string typeName)
        {
            Ensure.NotNull(type, "type");
            typeName = type.FullName + ", " + type.Assembly.GetName().Name;
            return true;
        }
    }
}
