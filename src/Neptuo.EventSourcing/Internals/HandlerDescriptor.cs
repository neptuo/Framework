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
        /// The type of the parameter to the execute method.
        /// </summary>
        public Type ArgumentType { get; private set; }

        /// <summary>
        /// The method that executes handler with parameter.
        /// </summary>
        protected Action<object, object> ExecuteMethod { get; private set; }


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
        public HandlerDescriptor(object handler, Type argumentType, Action<object, object> execute, bool isPlain, bool isEnvelope, bool isContext)
        {
            Ensure.NotNull(handler, "handler");
            Ensure.NotNull(argumentType, "argumentType");
            Ensure.NotNull(execute, "execute");
            Handler = handler;
            ArgumentType = argumentType;
            ExecuteMethod = execute;
            IsPlain = isPlain;
            IsEnvelope = isEnvelope;
            IsContext = isContext;
        }

        /// <summary>
        /// Executes handler method with <paramref name="parameter"/>.
        /// </summary>
        /// <param name="parameter">The argument to the handle method.</param>
        public void Execute(object parameter)
        {
            ExecuteMethod(Handler, parameter);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                hash = 13 * hash * Handler.GetHashCode();
                hash = 13 * hash * ArgumentType.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            HandlerDescriptor other = obj as HandlerDescriptor;
            if (other == null)
                return false;

            return Handler == other.Handler && ArgumentType == other.ArgumentType;
        }
    }
}
