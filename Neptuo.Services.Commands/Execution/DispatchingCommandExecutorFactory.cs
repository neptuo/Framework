using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Commands.Execution
{
    /// <summary>
    /// Eanbles registering factories for command types.
    /// </summary>
    public class DispatchingCommandExecutorFactory : ICommandExecutorFactory
    {
        /// <summary>
        /// Lost of registered factories for command types.
        /// </summary>
        protected Dictionary<Type, ICommandExecutorFactory> Factories { get; private set; }

        /// <summary>
        /// When factory for command type is not registered, this event gets fired with command instance.
        /// </summary>
        public Func<object, ICommandExecutorFactory> OnSearchFactory;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        public DispatchingCommandExecutorFactory()
        {
            Factories = new Dictionary<Type, ICommandExecutorFactory>();
        }

        /// <summary>
        /// Adds factory for commands of type <paramref name="commandType"/>.
        /// </summary>
        /// <param name="commandType">Command type.</param>
        /// <param name="factory">Command executor factory for commands of type <paramref name="commandType"/>.</param>
        /// <returns>This (fluently).</returns>
        public DispatchingCommandExecutorFactory AddFactory(Type commandType, ICommandExecutorFactory factory)
        {
            Ensure.NotNull(commandType, "commandType");
            Ensure.NotNull(factory, "factory");
            Factories[commandType] = factory;
            return this;
        }

        /// <summary>
        /// Seaches for <see cref="ICommandExecutorFactory"/> in registered factories or using <see cref="OnSearchFactory"/>.
        /// </summary>
        /// <param name="command">Command instance.</param>
        /// <returns>Command executor.</returns>
        /// <exception cref="CommandExecutorException">When factory lookup failed.</exception>
        public ICommandExecutor CreateExecutor(object command)
        {
            Ensure.NotNull(command, "command");
            Type commandType = command.GetType();
            ICommandExecutorFactory factory;

            if (Factories.TryGetValue(commandType, out factory))
                return factory.CreateExecutor(command);
    
            if(OnSearchFactory != null)
            {
                factory = OnSearchFactory(command);
                if (factory != null)
                    return factory.CreateExecutor(command);
            }

            throw new CommandExecutorException(String.Format("Unnable to find factory for command of type '{0}'.", commandType.FullName));
        }
    }
}
