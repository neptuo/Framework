using Mirrored.SharpKit.JavaScript;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.SharpKit.Exugin.Exports
{
    public class TypeRegistryItem
    {
        public bool? AutomaticPropertiesAsFields { get; set; }
        [DefaultValue(true)]
        public bool Export { get; set; }
        public JsMode? Mode { get; set; }
        public string Name { get; set; }
        public int? OrderInFile { get; set; }
        public bool? PropertiesAsFields { get; set; }
        public string TargetType { get; set; }

        public IEnumerable<MethodRegistryItem> Methods { get; set; }
    }
}
