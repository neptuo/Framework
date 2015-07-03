﻿using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Events.Handlers
{
    /// <summary>
    /// Describes whole event processing context.
    /// </summary>
    /// <typeparam name="TEvent">Type of event to handle.</typeparam>
    public interface IEventHandlerContext<TEvent>
    {
        /// <summary>
        /// Event data wrapped in envelope.
        /// </summary>
        Envelope<TEvent> Payload { get; }

        /// <summary>
        /// Current collection of event subscriptions.
        /// </summary>
        IEventHandlerCollection EventHandlers { get; }

        /// <summary>
        /// Current event dispatcher.
        /// </summary>
        IEventDispatcher Dispatcher { get; }
    }
}
