using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    /// <summary>
    /// A context for marking exception as handled.
    /// </summary>
    public interface IExceptionHandlerContext
    {
        /// <summary>
        /// Gets or sets whether the exception is handled.
        /// </summary>
        bool IsHandled { get; }

        /// <summary>
        /// Gets a collection of metadata for handling the exception.
        /// </summary>
        IKeyValueCollection Metadata { get; }

        /// <summary>
        /// Gets an instance of the exception.
        /// </summary>
        Exception Exception { get; }
    }

    /// <summary>
    /// A context for marking exception as handled.
    /// </summary>
    /// <typeparam name="T">A type of the exception.</typeparam>
    public interface IExceptionHandlerContext<T>
        where T : Exception
    {
        /// <summary>
        /// Gets or sets whether the exception is handled.
        /// </summary>
        bool IsHandled { get; }

        /// <summary>
        /// Gets a collection of metadata for handling the exception.
        /// </summary>
        IKeyValueCollection Metadata { get; }

        /// <summary>
        /// Gets an instance of the exception.
        /// </summary>
        T Exception { get; }
    }
}
