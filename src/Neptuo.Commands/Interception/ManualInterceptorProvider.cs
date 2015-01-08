using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Interception
{
    /// <summary>
    /// Enables static manual interception registration.
    /// For now, supports only interceptors for handler types, not handler methods.
    /// </summary>
    public class ManualInterceptorProvider : IInterceptorProvider
    {
        private IDependencyProvider dependencyProvider;

        /// <summary>
        /// Key is command handler type, value if list of interceptor factories.
        /// </summary>
        protected Dictionary<Type, List<Func<IDependencyProvider, IDecoratedInvoke>>> Storage { get; private set; }

        public ManualInterceptorProvider(IDependencyProvider dependencyProvider)
        {
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;

            Storage = new Dictionary<Type, List<Func<IDependencyProvider, IDecoratedInvoke>>>();
        }

        /// <summary>
        /// Registers interceptor <paramref name="interceptorType"/> for <paramref name="commandHandlerType"/>.
        /// </summary>
        /// <param name="commandHandlerType">Command handler type.</param>
        /// <param name="interceptorType">Interceptor type.</param>
        /// <rereturns>This (fluently).</rereturns>
        public ManualInterceptorProvider AddInterceptorType(Type commandHandlerType, Type interceptorType)
        {
            Guard.NotNull(commandHandlerType, "commandHandlerType");
            Guard.NotNull(interceptorType, "interceptorType");
            return AddInterceptorFactory(commandHandlerType, provider => (IDecoratedInvoke)provider.Resolve(interceptorType));
        }

        /// <summary>
        /// Registers interceptor factory <paramref name="factory"/> for <paramref name="commandHandlerType"/>.
        /// </summary>
        /// <param name="commandHandlerType">Command handler type.</param>
        /// <param name="interceptorType">Interceptor factory.</param>
        /// <rereturns>This (fluently).</rereturns>
        public ManualInterceptorProvider AddInterceptorFactory(Type commandHandlerType, Func<IDependencyProvider, IDecoratedInvoke> factory)
        {
            Guard.NotNull(commandHandlerType, "commandHandlerType");
            Guard.NotNull(factory, "factory");

            List<Func<IDependencyProvider, IDecoratedInvoke>> interceptorTypes;
            if (!Storage.TryGetValue(commandHandlerType, out interceptorTypes))
                Storage[commandHandlerType] = interceptorTypes = new List<Func<IDependencyProvider, IDecoratedInvoke>>();

            interceptorTypes.Add(factory);
            return this;
        }

        public IEnumerable<IDecoratedInvoke> GetInterceptors(object commandHandler, object command, MethodInfo commandHandlerMethod)
        {
            Guard.NotNull(commandHandler, "commandHandler");
            Type commandHandlerType = commandHandler.GetType();

            List<Func<IDependencyProvider, IDecoratedInvoke>> interceptorFactories;
            if (!Storage.TryGetValue(commandHandlerType, out interceptorFactories))
                interceptorFactories = new List<Func<IDependencyProvider, IDecoratedInvoke>>();

            List<IDecoratedInvoke> result = new List<IDecoratedInvoke>();
            foreach (Func<IDependencyProvider, IDecoratedInvoke> interceptorFactory in interceptorFactories)
                result.Add(interceptorFactory(dependencyProvider));
            
            return result;
        }
    }
}
