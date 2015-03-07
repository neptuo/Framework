using Neptuo.Pipelines.Commands.Handlers;
using Neptuo.Pipelines.Commands.Interception;
using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Activators;

namespace Neptuo.Pipelines.Commands.Execution
{
    /// <summary>
    /// <see cref="ICommandExecutor"/> using <see cref="IDependencyProvider"/>.
    /// Enables interception.
    /// </summary>
    public class DependencyCommandExecutor : ICommandExecutor, IDecoratedInvoke
    {
        /// <summary>
        /// Name of <see cref="ICommandHandler{TCommand}.Handle"/>.
        /// </summary>
        private static readonly string handleMethodName = TypeHelper.MethodName<ICommandHandler<object>, object>(h => h.Handle);

        /// <summary>
        /// Current dependency provider.
        /// </summary>
        private IDependencyProvider dependencyProvider;

        /// <summary>
        /// Current provider for interceptors.
        /// </summary>
        private IInterceptorProvider interceptorProvider;

        /// <summary>
        /// Fired when handling of command was completed.
        /// </summary>
        public event Action<ICommandExecutor, object> OnCommandHandled;

        /// <summary>
        /// Initializes new instance with <paramref name="dependencyProvider"/>.
        /// </summary>
        /// <param name="dependencyProvider">Source for registrations.</param>
        public DependencyCommandExecutor(IDependencyProvider dependencyProvider, IInterceptorProvider interceptorProvider)
        {
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            Guard.NotNull(interceptorProvider, "interceptorProvider");
            this.dependencyProvider = dependencyProvider;
            this.interceptorProvider = interceptorProvider;
        }

        /// <summary>
        /// Handles <paramref name="command"/>.
        /// </summary>
        /// <param name="command">Command to handle.</param>
        public void Handle(object command)
        {
            Guard.NotNull(command, "command");
            Type commandType = command.GetType();

            Type genericHandlerType = typeof(ICommandHandler<>);
            Type concreteHandlerType = genericHandlerType.MakeGenericType(commandType);
            MethodInfo methodInfo = concreteHandlerType.GetMethod(handleMethodName);
            object commandHandler = dependencyProvider.Resolve(concreteHandlerType);

            List<IDecoratedInvoke> interceptors = new List<IDecoratedInvoke>(interceptorProvider.GetInterceptors(commandHandler, command, methodInfo));
            interceptors.Add(this);

            InterceptorExectionContext context = new InterceptorExectionContext(interceptors, commandHandler, command);
            context.Next();

            if (context.Exception == null)
            {
                if (OnCommandHandled != null)
                    OnCommandHandled(this, context.Command);
            }
            else
            {
                throw context.Exception;
            }
        }

        /// <summary>
        /// When all interceptors are done, executes command.
        /// Requires <paramref name="context"/> to be of type <see cref="ICommandHandlerAware"/>.
        /// </summary>
        /// <param name="context">Context object, must be of type <see cref="ICommandHandlerAware"/>.</param>
        public void OnInvoke(IDecoratedInvokeContext context)
        {
            ICommandHandlerAware collection = context as ICommandHandlerAware;
            if (collection == null)
                throw new CommandExecutorException(String.Format("Context is not of type '{0}'.", typeof(ICommandHandlerAware).FullName));

            try
            {
                MethodInfo methodInfo = collection.CommandHandler.GetType().GetMethod(handleMethodName);
                methodInfo.Invoke(collection.CommandHandler, new[] { context.Command });
            }
            catch (TargetInvocationException e)
            {
                context.Exception = e.InnerException;
            }
        }
    }
}
