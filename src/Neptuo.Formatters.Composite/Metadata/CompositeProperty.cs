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
        public string Name { get; private set; }
        public Type Type { get; private set; }
        public Func<object, object> Getter { get; private set; }
        public Action<object, object> Setter { get; private set; }

        public CompositeProperty(int index, string name, Type type, Func<object, object> getter)
        {
            Ensure.PositiveOrZero(index, "index");
            Ensure.NotNullOrEmpty(name, "name");
            Ensure.NotNull(type, "type");
            Ensure.NotNull(getter, "getter");
            Index = index;
            Name = name;
            Type = type;
            Getter = getter;
        }

        public CompositeProperty(int index, string name, Type type, Func<object, object> getter, Action<object, object> setter)
            : this(index, name, type, getter)
        {
            Ensure.NotNull(setter, "setter");
            Setter = setter;
        }
    }
}
