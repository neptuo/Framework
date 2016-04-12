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

        public HandlerDescriptor Get(object handler, Type argumentType)
        {
            Ensure.NotNull(handler, "handler");
            Type handlerType = handler.GetType();

            string handlerIdentifier = FindIdentifier(handlerType);
            
            MethodInfo method = handlerType.GetInterfaceMap(interfaceType.MakeGenericType(argumentType)).TargetMethods
                .FirstOrDefault(m => m.Name.EndsWith(methodName));

            ArgumentDescriptor argument = Get(argumentType);
            return new HandlerDescriptor(
                handlerIdentifier,
                handler,
                argument.ArgumentType,
                (h, p) => (Task)method.Invoke(h, new object[] { p }),
                argument.IsPlain,
                argument.IsEnvelope,
                argument.IsContext
            );
        }

        public ArgumentDescriptor Get(Type argumentType)
        {
            bool isEnvelope = false;
            bool isContext = false;
            if (argumentType.IsGenericType)
            {
                Type argumentGenericType = argumentType.GetGenericTypeDefinition();
                Type[] arguments = argumentType.GetGenericArguments();
                if (arguments.Length == 1)
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

            return new ArgumentDescriptor(
                argumentType,
                isEnvelope || isContext,
                isEnvelope,
                isContext
            );
        }

        public Type UnWrapArgumentType(Type argumentType)
        {
            if (argumentType.IsGenericType)
            {
                Type argumentGenericType = argumentType.GetGenericTypeDefinition();
                Type[] arguments = argumentType.GetGenericArguments();
                if (arguments.Length == 1)
                {
                    if (argumentGenericType == contextGenericType)
                        argumentType = arguments[0];
                    else if (argumentGenericType == typeof(Envelope<>))
                        argumentType = arguments[0];
                }
            }

            return argumentType;
        }

        private string FindIdentifier(Type handlerType)
        {
            string identifier = handlerType.AssemblyQualifiedName;
            IdentifierAttribute attribute = handlerType.GetCustomAttribute<IdentifierAttribute>();
            if (attribute != null)
                identifier = attribute.Value;

            return identifier;
        }
    }
}
