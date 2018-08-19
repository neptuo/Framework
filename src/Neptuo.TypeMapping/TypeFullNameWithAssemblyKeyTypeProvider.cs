using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// An implementation of <see cref="IKeyTypeProvider"/> which uses <see cref="Type.FullName"/> and <see cref="Type.Assembly.GetName().Name"/>, separated by comma and space.
    /// </summary>
    public class TypeFullNameWithAssemblyKeyTypeProvider : IKeyTypeProvider
    {
        public bool TryGet(string keyType, out Type type)
        {
            Ensure.NotNullOrEmpty(keyType, "keyType");
            type = Type.GetType(keyType);
            return type != null;
        }

        public bool TryGet(Type type, out string keyType)
        {
            Ensure.NotNull(type, "type");
            keyType = type.FullName + ", " + type.Assembly.GetName().Name;
            return true;
        }
    }
}
