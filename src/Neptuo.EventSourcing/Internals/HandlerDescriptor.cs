using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Internals
{
    /// <summary>
    /// Provides description of (command or event) handler.
    /// </summary>
    internal class HandlerDescriptor
    {
        /// <summary>
        /// The instance itself.
        /// </summary>
        public object Handler { get; private set; }

        /// <summary>
        /// The method that executes handler with parameter.
        /// </summary>
        public Action<object> Execute { get; private set; }


        /// <summary>
        /// Whether is plain command/event handler.
        /// </summary>
        public bool IsPlain { get; private set; }

        /// <summary>
        /// Whether requires envelope wrapper.
        /// </summary>
        public bool IsEnvelope { get; private set; }

        /// <summary>
        /// Whether required context wrapper.
        /// </summary>
        public bool IsContext { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        public HandlerDescriptor(object handler, Action<object> execute, bool isPlain, bool isEnvelope, bool isContext)
        {
            Ensure.NotNull(handler, "handler");
            Ensure.NotNull(execute, "execute");
            Handler = handler;
            Execute = execute;
            IsPlain = isPlain;
            IsEnvelope = isEnvelope;
            IsContext = isContext;
        }
    }
}
