using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ImportTypeAttribute : Attribute
    { 
        public Type Type { get; private set; }

        public ImportTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}
