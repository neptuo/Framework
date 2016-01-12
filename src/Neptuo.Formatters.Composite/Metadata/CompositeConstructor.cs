using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Metadata
{
    public class CompositeConstructor
    {
        public Func<object[], object> Factory { get; private set; }

        public CompositeConstructor(Func<object[], object> factory)
        {
            Ensure.NotNull(factory, "factory");
            Factory = factory;
        }
    }
}
