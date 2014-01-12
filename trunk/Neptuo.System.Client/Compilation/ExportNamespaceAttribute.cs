using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpKit.JavaScript;

namespace Neptuo.Client.Compilation
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = true, AllowMultiple = true)]
    [JsType(Export = false)]
    public sealed class ExportNamespaceAttribute : Attribute
    {
        public ExportNamespaceAttribute(string fileName, string ns)
        { }
    }
}
