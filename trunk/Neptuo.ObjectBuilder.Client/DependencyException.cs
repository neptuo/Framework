using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ObjectBuilder.Client
{
    [Serializable]
    public class DependencyException : Exception
    {
        public DependencyException(string message)
            : base(message)
        { }
    }
}
