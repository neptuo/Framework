using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Events.Handlers
{
    /// <summary>
    /// Handler for events of type <typeparamref name="TEvent"/>.
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<TEvent>
    {
        /// <summary>
        /// Handles event described by <paramref name="eventData"/>.
        /// </summary>
        /// <param name="eventData">Instance of event data.</param>
        void Handle(TEvent eventData);
    }
}
