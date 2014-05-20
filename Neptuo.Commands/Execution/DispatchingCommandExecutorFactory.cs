using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Execution
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
        /// Seaches for <see cref="ICommandExecutorFactory"/> in registered factories or using <see cref="OnSearchFactory"/>.
        /// </summary>
        /// <param name="command">Command instance.</param>
        /// <returns>Command executor.</returns>
        /// <exception cref="CommandExecutorFactoryException">When factory lookup failed.</exception>
        public ICommandExecutor CreateExecutor(object command)
        {
            Guard.NotNull(command, "command");
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

            throw new CommandExecutorFactoryException(String.Format("Unnable to find factory for command of type '{0}'.", commandType.FullName));
        }
    }
}
