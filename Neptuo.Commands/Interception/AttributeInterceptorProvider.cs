using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Interception
{
    public class AttributeInterceptorProvider : IInterceptorProvider
    {
        public IEnumerable<IDecoratedInvoke> GetInterceptors(object commandHandler)
        {
            Guard.NotNull(commandHandler, "commandHandler");
            List<IDecoratedInvoke> result = new List<IDecoratedInvoke>();
            foreach (Attribute attribute in commandHandler.GetType().GetCustomAttributes(true))
            {
                IDecoratedInvoke interceptor = attribute as IDecoratedInvoke;
                if (interceptor != null)
                    result.Add(interceptor);
            }
            return result;
        }
    }
}
