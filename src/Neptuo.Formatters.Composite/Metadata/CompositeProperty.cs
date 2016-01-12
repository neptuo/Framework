using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Metadata
{
    public class CompositeProperty
    {
        public int Index { get; private set; }
        public PropertyInfo Property { get; private set; }

        public CompositeProperty(int index, PropertyInfo property)
        {
            Ensure.PositiveOrZero(index, "index");
            Ensure.NotNull(property, "property");
            Property = property;
        }
    }
}
