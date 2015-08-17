using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ExportTypeAttribute : Attribute
    {
        public Type Type { get; private set; }

        public ExportTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}
