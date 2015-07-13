using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Defines property, that should be filled by DI container.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DependencyAttribute : Attribute
    { }
}
