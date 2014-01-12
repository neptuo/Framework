using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ObjectBuilder.Client
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ExportAttribute : Attribute
    {
        public ExportAttribute(string namespaceName, string exportFile)
        { }
    }
}
