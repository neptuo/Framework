using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// A provider for mapping from <see cref="Type"/> to <see cref="IKey.Type"/> and vice-versa.
    /// </summary>
    public interface IKeyTypeProvider
    {
        bool TryGet(string keyType, out Type type);
        bool TryGet(Type type, out string keyType);
    }
}
