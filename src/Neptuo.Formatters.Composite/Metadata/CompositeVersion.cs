using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Metadata
{
    public class CompositeVersion
    {
        public int Version { get; private set; }
        public CompositeConstructor Constructor { get; private set; }
        public IEnumerable<CompositeProperty> Properties { get; private set; }

        public CompositeVersion(int version, CompositeConstructor constructor, IEnumerable<CompositeProperty> properties)
        {
            Ensure.Positive(version, "version");
            Ensure.NotNull(constructor, "constructor");
            Ensure.NotNull(properties, "properties");
            Version = version;
            Constructor = constructor;
            Properties = properties;
        }
    }
}
