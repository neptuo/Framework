using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Metadata
{
    public class CompositeType
    {
        public string Name { get; private set; }
        public Type Type { get; private set; }
        public IEnumerable<CompositeVersion> Versions { get; private set; }

        public CompositeProperty VersionProperty { get; private set; }

        public CompositeType(string name, Type type, IEnumerable<CompositeVersion> versions, CompositeProperty versionProperty)
        {
            Ensure.NotNullOrEmpty(name, "name");
            Ensure.NotNull(type, "type");
            Ensure.NotNull(versions, "versions");
            Ensure.NotNull(versionProperty, "versionProperty");
            Name = name;
            Type = type;
            Versions = versions;
            VersionProperty = versionProperty;
        }
    }
}
