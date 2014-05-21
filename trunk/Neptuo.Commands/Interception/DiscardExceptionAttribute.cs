using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Interception
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DiscardExceptionAttribute : Attribute, IDecoratedInvoke
    {
        public IEnumerable<Type> Exceptions { get; private set; }

        public DiscardExceptionAttribute(params Type[] execeptions)
        {
            Exceptions = execeptions ?? Enumerable.Empty<Type>();
        }

        public void OnInvoke(IDecoratedInvokeContext context)
        {
            try
            {
                context.Next();
            }
            catch (Exception e)
            {
                TargetInvocationException reflectionException = e as TargetInvocationException;
                if (reflectionException != null)
                    e = reflectionException;

                if (Exceptions.Contains(e.GetType()))
                    return;

                throw e;
            }
        }
    }
}
