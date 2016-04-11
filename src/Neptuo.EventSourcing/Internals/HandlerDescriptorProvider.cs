using Neptuo.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Internals
{
    internal class HandlerDescriptorProvider
    {
        private readonly Type interfaceType;
        private readonly Type contextGenericType;
        private readonly string methodName;

        public HandlerDescriptorProvider(Type interfaceType, Type contextGenericType, string methodName)
        {
            Ensure.NotNull(interfaceType, "interfaceType");
            Ensure.NotNull(contextGenericType, "contextGenericType");
            Ensure.NotNullOrEmpty(methodName, "methodName");
            this.interfaceType = interfaceType;
            this.contextGenericType = contextGenericType;
            this.methodName = methodName;
        }

        public HandlerDescriptor Create(object handler, Type argumentType)
        {
            Ensure.NotNull(handler, "handler");
            Type handlerType = handler.GetType();
            
            MethodInfo method = handlerType.GetInterfaceMap(interfaceType.MakeGenericType(argumentType)).TargetMethods
                .FirstOrDefault(m => m.Name.EndsWith(methodName));

            bool isEnvelope = false;
            bool isContext = false;
            if (argumentType.IsGenericType)
            {
                Type argumentGenericType = argumentType.GetGenericTypeDefinition();
                Type[] arguments = argumentType.GetGenericArguments();
                if(arguments.Length == 1)
                {
                    isContext = argumentGenericType == contextGenericType;
                    if (isContext)
                    {
                        argumentType = arguments[0];
                    }
                    else
                    {
                        isEnvelope = argumentGenericType == typeof(Envelope<>);
                        if (isEnvelope)
                            argumentType = arguments[0];
                    }
                }
            }

            return new HandlerDescriptor(
                handler,
                argumentType,
                (h, p) => method.Invoke(h, new object[] { p }),
                isEnvelope || isContext,
                isEnvelope,
                isContext
            );
        }
    }
}
