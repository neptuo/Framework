using Neptuo.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Services.Queries
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class LogAttribute : Attribute
    { }

    public class LogBehavior : IBehavior<object>
    {
        public Task ExecuteAsync(object handler, IBehaviorContext context)
        {
            Console.WriteLine("Executing method on '{0}'.", handler.GetType().FullName);
            return context.NextAsync();
        }
    }
}
