using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Commands.Interception
{
    /// <summary>
    /// Interceptor that discards selected exceptions.
    /// Execution will behave like these exceptions never occur.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DiscardExceptionAttribute : Attribute, IDecoratedInvoke
    {
        /// <summary>
        /// List of seleted exception types.
        /// </summary>
        public IEnumerable<Type> Exceptions { get; private set; }

        /// <summary>
        /// Creates new instance with selected exception types to discard.
        /// </summary>
        /// <param name="execeptions"></param>
        public DiscardExceptionAttribute(params Type[] execeptions)
        {
            Exceptions = execeptions ?? Enumerable.Empty<Type>();
        }

        public void OnInvoke(IDecoratedInvokeContext context)
        {
            context.Next();

            if (context.Exception != null && Exceptions.Contains(context.Exception.GetType()))
                context.Exception = null;
        }
    }
}
