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
        public Func<object, object> Getter { get; private set; }
        public Action<object, object> Setter { get; private set; }

        public CompositeProperty(int index, Func<object, object> getter)
        {
            Ensure.PositiveOrZero(index, "index");
            Ensure.NotNull(getter, "getter");
            Getter = getter;
        }

        public CompositeProperty(int index, Func<object, object> getter, Action<object, object> setter)
        {
            Ensure.PositiveOrZero(index, "index");
            Ensure.NotNull(getter, "getter");
            Ensure.NotNull(setter, "setter");
            Getter = getter;
            Setter = setter;
        }
    }
}
