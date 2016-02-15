using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    /// <summary>
    /// The implementation of <see cref="ICommandDispatcher"/> which executes all commands in serie (one by one - using queue).
    /// Commands are processed in separate thread and <see cref="HandleAsync"/> returns control right after storing command for dispatching.
    /// </summary>
    public class SerialCommandDispatcher : ICommandDispatcher
    {
        private readonly Queue<object> storage = new Queue<object>();
        private readonly object storageLock = new object();

        public Task HandleAsync<TCommand>(TCommand command)
        {


            throw new NotImplementedException();
        }
    }
}
