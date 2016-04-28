using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    /// <summary>
    /// The builder of various filters for handling specific exceptions.
    /// </summary>
    public class ExceptionHandlerBuilder : ExceptionHandlerBuilder<Exception>
    {
        public ExceptionHandlerBuilder Handler<T>(IExceptionHandler<T> handler)
            where T : Exception
        {
            ChildHandlers.Add(new ExceptionHandlerBuilder<T>().Handler(handler));
            return this;
        }

        public ExceptionHandlerBuilder Handler<T>(Action<T> handler)
            where T : Exception
        {
            ChildHandlers.Add(new ExceptionHandlerBuilder<T>().Handler(handler));
            return this;
        }
    }

    /// <summary>
    /// The builder of various filters for handling specific exceptions.
    /// </summary>
    public class ExceptionHandlerBuilder<T> : IExceptionHandler
        where T : Exception
    {
        private List<Func<T, bool>> filters = new List<Func<T, bool>>();
        private List<IExceptionHandler<T>> handlers = new List<IExceptionHandler<T>>();

        protected List<IExceptionHandler> ChildHandlers = new List<IExceptionHandler>();

        internal ExceptionHandlerBuilder()
        { }

        internal ExceptionHandlerBuilder(Func<T, bool> filter)
        {
            filters.Add(filter);
        }

        /// <summary>
        /// Creates sub-builder for exceptions of type <typeparamref name="TBase"/>.
        /// </summary>
        /// <typeparam name="TBase">The type of the exception to handle.</typeparam>
        /// <returns>New sub-builder for exceptions of type <typeparamref name="TBase"/>.</returns>
        public ExceptionHandlerBuilder<TBase> Filter<TBase>()
            where TBase : T
        {
            ExceptionHandlerBuilder<TBase> builder = new ExceptionHandlerBuilder<TBase>();
            ChildHandlers.Add(builder);
            return builder;
        }

        public ExceptionHandlerBuilder<T> Filter(Func<T, bool> filter)
        {
            Ensure.NotNull(filter, "filter");

            if(handlers.Count > 0 || ChildHandlers.Count > 0)
            {
                ExceptionHandlerBuilder<T> builder = new ExceptionHandlerBuilder<T>(filter);
                ChildHandlers.Add(builder);
                return builder;
            }

            filters.Add(filter);
            return this;
        }

        public ExceptionHandlerBuilder<T> Handler(IExceptionHandler<T> handler)
        {
            Ensure.NotNull(handler, "handler");
            handlers.Add(handler);
            return this;
        }

        public ExceptionHandlerBuilder<T> Handler(Action<T> handler)
        {
            handlers.Add(DelegateExceptionHandler.FromAction<T>(handler));
            return this;
        }

        void IExceptionHandler.Handle(Exception exception)
        {
            Ensure.NotNull(exception, "exception");

            // TODO: Maintain the order of calling Filter and Handler to support more and more specific filtering 
            // with support for handlers at different level of this specificity.

            T ex = exception as T;
            if (ex == null)
                return;

            // If no filters are defined or all filters are passed, handler the exception.
            bool isPassed = !filters.Any() || filters.All(f => f(ex));
            if(isPassed)
            {
                // First, handle in all registered handlers.
                foreach (IExceptionHandler<T> handler in handlers)
                    handler.Handle(ex);

                // Second, handle in all child builders.
                foreach (IExceptionHandler childHandler in ChildHandlers)
                    childHandler.Handle(ex);
            }
        }
    }
}
