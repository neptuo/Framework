using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    /// <summary>
    /// A builder for <see cref="DefaultConverterRepository"/>.
    /// </summary>
    public class DefaultConverterRepositoryBuilder : IFactory<IConverterRepository>
    {
        private IConverterRepository inner;
        private Dictionary<Type, Dictionary<Type, IConverter>> storage;
        private Action<Exception> exceptionHandler;

        /// <summary>
        /// Sets an inner repository to use.
        /// </summary>
        /// <param name="repository">An instance of inner repository or <c>null</c> to clear inner repository.</param>
        /// <returns>Self (for fluency).</returns>
        public DefaultConverterRepositoryBuilder UseInner(IConverterRepository repository)
        {
            this.inner = repository;
            return this;
        }

        /// <summary>
        /// Sets a converter mapping storage.
        /// </summary>
        /// <param name="storage">An instance of mapping storage or <c>null</c> to clear mapping storage.</param>
        /// <returns>Self (for fluency).</returns>
        public DefaultConverterRepositoryBuilder UseStorage(Dictionary<Type, Dictionary<Type, IConverter>> storage)
        {
            this.storage = storage;
            return this;
        }

        /// <summary>
        /// Sets an exception handler.
        /// </summary>
        /// <param name="exceptionHandler">A delegate for handling exceptions or <c>null</c> to clear exception handling.</param>
        /// <returns>Self (for fluency).</returns>
        public DefaultConverterRepositoryBuilder UseExceptionHandler(Action<Exception> exceptionHandler)
        {
            this.exceptionHandler = exceptionHandler;
            return this;
        }

        /// <summary>
        /// Sets an exception handler that sinks all exceptions.
        /// </summary>
        /// <returns>Self (for fluency).</returns>
        public DefaultConverterRepositoryBuilder UseExceptionSink()
        {
            return UseExceptionHandler(e => { });
        }

        /// <summary>
        /// Creates a new instance of a <see cref="DefaultConverterRepository"/> from current parameters.
        /// </summary>
        /// <returns>A new instance of a <see cref="DefaultConverterRepository"/> with current parameters.</returns>
        public IConverterRepository Create()
        {
            if (exceptionHandler == null)
            {
                if (storage != null)
                    return new DefaultConverterRepository(storage);

                if (inner != null)
                    return new DefaultConverterRepository(inner);
            }
            else
            {
                if (storage != null)
                    return new DefaultConverterRepository(storage, exceptionHandler);

                if (inner != null)
                    return new DefaultConverterRepository(inner, exceptionHandler);

                return new DefaultConverterRepository(exceptionHandler);
            }

            throw Ensure.Exception.NotImplemented("Invalid state of the 'DefaultConverterRepositoryBuilder'.");
        }
    }
}
