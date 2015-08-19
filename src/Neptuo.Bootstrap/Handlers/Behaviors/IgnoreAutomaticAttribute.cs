using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Handlers.Behaviors
{
    /// <summary>
    /// Bootstrap handlers decorated with this attribute are not executed in automatic bootstrappers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class IgnoreAutomaticAttribute : Attribute
    { }
}
