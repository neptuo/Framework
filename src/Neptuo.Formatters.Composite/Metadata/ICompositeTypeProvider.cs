using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Metadata
{
    public interface ICompositeTypeProvider
    {
        bool TryGet(Type type, out CompositeType definition);

        bool TryGet(string typeName, out CompositeType definition);
    }
}
