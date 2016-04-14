using Neptuo.Commands;
using Neptuo.Events;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Domains
{
    /// <summary>
    /// The base class for (long running) process model.
    /// </summary>
    public class ProcessRoot : AggregateRoot, IProcessRoot
    {
        private readonly List<ICommand> commands = new List<ICommand>();

        /// <summary>
        /// The enumeration of unpublished commands.
        /// </summary>
        public IEnumerable<ICommand> Commands
        {
            get { return commands; }
        }

        public ProcessRoot()
            : base()
        {
            EnsureHandlerRegistration();
        }

        public ProcessRoot(IKey key, IEnumerable<IEvent> events)
            : base(key, events)
        {
            EnsureHandlerRegistration();
        }

        private void EnsureHandlerRegistration()
        {
            
        }
    }
}
