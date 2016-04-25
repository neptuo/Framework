using Neptuo.Exceptions;
using Neptuo.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Internals
{
    /// <summary>
    /// Builder for <see cref="HandlerDescriptor"/> and <see cref="ArgumentDescriptor"/>.
    /// </summary>
    internal class HandlerDescriptorProvider
    {
        private readonly Type interfaceType;
        private readonly Type contextGenericType;
        private readonly string methodName;
        private readonly IExceptionHandlerCollection exceptionHandlers;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="interfaceType">The type of interface for handler. Used to find execute method for <see cref="HandlerDescriptor"/>.</param>
        /// <param name="contextGenericType">The type of the generic interface for context.</param>
        /// <param name="methodName">The 'executed' method name.</param>
        public HandlerDescriptorProvider(Type interfaceType, Type contextGenericType, string methodName, IExceptionHandlerCollection exceptionHandlers)
        {
            Ensure.NotNull(interfaceType, "interfaceType");
            Ensure.NotNullOrEmpty(methodName, "methodName");
            Ensure.NotNull(exceptionHandlers, "exceptionHandlers");
            this.interfaceType = interfaceType;
            this.contextGenericType = contextGenericType;
            this.methodName = methodName;
            this.exceptionHandlers = exceptionHandlers;
        }

        /// <summary>
        /// Builds <see cref="HandlerDescriptor"/> for <paramref name="handler"/> and its <paramref name="argumentType"/> (wrapper or unwrapper).
        /// </summary>
        /// <param name="handler">The handler to build descriptor for.</param>
        /// <param name="argumentType">The argument type (possibly wrapped in envelope or context).</param>
        /// <returns>The instance of the handler descriptor.</returns>
        public HandlerDescriptor Get(object handler, Type argumentType)
        {
            Ensure.NotNull(handler, "handler");
            Type handlerType = handler.GetType();

            string handlerIdentifier = FindIdentifier(handlerType);
            
            MethodInfo method = handlerType.GetInterfaceMap(interfaceType.MakeGenericType(argumentType)).TargetMethods
                .FirstOrDefault(m => m.Name.EndsWith(methodName));

            Func<object, object, Task> handlerAction = (h, p) =>
            {
                Task result = (Task)method.Invoke(h, new object[] { p });
                result = result.ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        exceptionHandlers.Handle(task.Exception.InnerException);
                        return Async.CompletedTask;
                    }

                    return task;
                });
                return result;
            };

            ArgumentDescriptor argument = Get(argumentType);
            return new HandlerDescriptor(
                handlerIdentifier,
                handler,
                argument.ArgumentType,
                handlerAction,
                argument.IsPlain,
                argument.IsEnvelope,
                argument.IsContext
            );
        }

        /// <summary>
        /// Builds <see cref="ArgumentDescriptor"/> for <paramref name="argumentType"/>.
        /// </summary>
        /// <param name="argumentType">The argument type (possibly wrapped in envelope or context).</param>
        /// <returns>The instance of the argument descriptor.</returns>
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
                    isContext = contextGenericType == null ? false : argumentGenericType == contextGenericType;
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
                !isEnvelope && !isContext,
                isEnvelope,
                isContext
            );
        }

        /// <summary>
        /// Reads the identifier of the <paramref name="handlerType"/>.
        /// By default the identifier is <see cref="Type.AssemblyQualifiedName"/>, but
        /// can be overriden using <see cref="IdentifierAttribute"/>.
        /// </summary>
        /// <param name="handlerType">The handler to determine the identifier of.</param>
        /// <returns>The identifier of the <paramref name="handlerType"/> or <c>null</c>.</returns>
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
