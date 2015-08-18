using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Test1Attribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.Class)]
    public class Test2Attribute : Attribute
    { }

    public class Test1Behavior : IBehavior<Target1>
    {
        public Task ExecuteAsync(Target1 handler, IBehaviorContext context)
        {
            handler.Test1 = true;
            return context.NextAsync();
        }
    }

    public class Test2Behavior : IBehavior<object>
    {
        public Task ExecuteAsync(object handler, IBehaviorContext context)
        {
            return context.NextAsync();
        }
    }

    [Test1]
    [Test2]
    public class Target1
    {
        public bool Test1 { get; set; }
        public bool Test2 { get; set; }
    }

    [Test2]
    public class Target2
    { }
}
