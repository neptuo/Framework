using Neptuo;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    /// <summary>
    /// A default implementation of the <see cref="IExceptionHandlerContext"/>.
    /// </summary>
    public class DefaultExceptionHandlerContext : IExceptionHandlerContext
    {
        private IKeyValueCollection metadata;

        public bool IsHandled { get; set; }
        public Exception Exception { get; private set; }

        public IKeyValueCollection Metadata
        {
            get
            {
                if (metadata == null)
                    metadata = new KeyValueCollection();

                return metadata;
            }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="exception">An exception to handle.</param>
        public DefaultExceptionHandlerContext(Exception exception)
        {
            Ensure.NotNull(exception, "exception");
            Exception = exception;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="exception">An exception to handle.</param>
        /// <param name="metadata">An initial collection of metadata.</param>
        public DefaultExceptionHandlerContext(Exception exception, IKeyValueCollection metadata)
        {
            Ensure.NotNull(exception, "exception");
            Ensure.NotNull(metadata, "metadata");
            Exception = exception;
            this.metadata = metadata;
        }
    }

    /// <summary>
    /// A default implementation of the <see cref="IExceptionHandlerContext"/>.
    /// </summary>
    public class DefaultExceptionHandlerContext<T> : IExceptionHandlerContext<T>
        where T : Exception
    {
        private IKeyValueCollection metadata;

        public bool IsHandled { get; set; }
        public T Exception { get; private set; }

        public IKeyValueCollection Metadata
        {
            get
            {
                if (metadata == null)
                    metadata = new KeyValueCollection();

                return metadata;
            }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="exception">An exception to handle.</param>
        public DefaultExceptionHandlerContext(T exception)
        {
            Ensure.NotNull(exception, "exception");
            Exception = exception;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="exception">An exception to handle.</param>
        /// <param name="metadata">An initial collection of metadata.</param>
        public DefaultExceptionHandlerContext(T exception, IKeyValueCollection metadata)
        {
            Ensure.NotNull(exception, "exception");
            Ensure.NotNull(metadata, "metadata");
            Exception = exception;
            this.metadata = metadata;
        }
    }
}
