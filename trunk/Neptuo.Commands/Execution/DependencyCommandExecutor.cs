using Neptuo.Commands.Handlers;
using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Execution
{
    /// <summary>
    /// <see cref="ICommandExecutor"/> using <see cref="IDependencyProvider"/>.
    /// </summary>
    public class DependencyCommandExecutor : ICommandExecutor
    {
        /// <summary>
        /// Name of <see cref="ICommandHandler<>.Handle"/>.
        /// </summary>
        private static readonly string handleMethodName = TypeHelper.MethodName<ICommandHandler<object>, object>(h => h.Handle);

        /// <summary>
        /// Current dependency provider.
        /// </summary>
        private IDependencyProvider dependencyProvider;

        /// <summary>
        /// Initializes new instance with <paramref name="dependencyProvider"/>.
        /// </summary>
        /// <param name="dependencyProvider">Source for registrations.</param>
        public DependencyCommandExecutor(IDependencyProvider dependencyProvider)
        {
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
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


            object commandHandler = dependencyProvider.Resolve(concreteHandlerType);
            MethodInfo methodInfo = concreteHandlerType.GetMethod(handleMethodName);
            methodInfo.Invoke(commandHandler, new[] { command });

            //ICommandHandler<TCommand> handler = dependencyProvider.Resolve<ICommandHandler<TCommand>>();
            //handler.Handle(command);
        }
    }
}
