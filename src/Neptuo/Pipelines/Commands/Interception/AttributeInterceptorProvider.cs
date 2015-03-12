using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Commands.Interception
{
    /// <summary>
    /// Reads interceptors from attributes used on target command handler.
    /// Attributes are read from class type and method definition.
    /// </summary>
    public class AttributeInterceptorProvider : IInterceptorProvider
    {
        public IEnumerable<IDecoratedInvoke> GetInterceptors(object commandHandler, object command, MethodInfo commandHandlerMethod)
        {
            Ensure.NotNull(commandHandler, "commandHandler");
            List<IDecoratedInvoke> result = new List<IDecoratedInvoke>();
            AppendInterceptors(commandHandler.GetType(), result);
            AppendInterceptors(commandHandlerMethod, result);

            return result;
        }

        private void AppendInterceptors(MemberInfo source, List<IDecoratedInvoke> result)
        {
            foreach (Attribute attribute in source.GetCustomAttributes(true))
            {
                IDecoratedInvoke interceptor = attribute as IDecoratedInvoke;
                if (interceptor != null)
                    result.Add(interceptor);
            }
        }
    }
}
