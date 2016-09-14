using Mirrored.SharpKit.JavaScript;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpKit.UnobtrusiveFeatures.Exports
{
    public class TypeRegistryItem : RegistryItemBase
    {
        public string Name { get; set; }
        public int? OrderInFile { get; set; }

        public IEnumerable<MethodRegistryItem> Methods { get; set; }
    }
}
