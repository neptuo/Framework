using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class ReturnTypeAttribute : Attribute
    {
        public Type Type { get; private set; }

        public ReturnTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}
