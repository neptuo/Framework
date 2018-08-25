using Neptuo.Exceptions.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions
{
    /// <summary>
    /// The exception-safe runner for actions and functions with support for reprocess.
    /// </summary>
    public class Reprocess
    {
        private readonly IExceptionHandler<IExceptionHandlerContext> handler;
        private readonly IExceptionHandler greedyHandler;
        private readonly int maxCount;

        /// <summary>
        /// Creates a new instance that uses <paramref name="handler"/> to pass all exceptions to.
        /// All exceptions are reprocessable.
        /// </summary>
        /// <param name="handler">An exception handler.</param>
        /// <param name="maxCount">A maximum count of re-run. Must be positive number.</param>
        public Reprocess(IExceptionHandler handler, int maxCount)
        {
            Ensure.NotNull(handler, "handler");
            Ensure.Positive(maxCount, "maxCount");
            this.greedyHandler = handler;
            this.maxCount = maxCount;
        }

        /// <summary>
        /// Creates a new instance that uses <paramref name="handler"/> to determine, which exceptions are reprocessable.
        /// If the <paramref name="handler"/> marks the exception as handled, the <see cref="Reprocess"/> tries to run the action again; otherwise immediately stops the execetion of action/function.
        /// </summary>
        /// <param name="handler">An exception handler.</param>
        /// <param name="maxCount">A maximum count of re-run. Must be positive number.</param>
        public Reprocess(IExceptionHandler<IExceptionHandlerContext> handler, int maxCount)
        {
            Ensure.NotNull(handler, "handler");
            Ensure.Positive(maxCount, "maxCount");
            this.handler = handler;
            this.maxCount = maxCount;
        }

        private bool IsReprocessable(Exception e, ref int count)
        {
            if (handler != null)
            {
                if (handler.TryHandle(e))
                {
                    count++;
                    return true;
                }
            }
            else if (greedyHandler != null)
            {
                greedyHandler.Handle(e);
                count++;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// Returns <c>true</c> if execution was successfull (within the max count of re-run); <c>false</c> otherwise.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <returns><c>true</c> if execution was successfull (within the max count of re-run); <c>false</c> otherwise.</returns>
        public bool Run(Action execute)
        {
            Ensure.NotNull(execute, "execute");

            int count = 0;
            while (count < maxCount)
            {
                try
                {
                    execute();
                    return true;
                }
                catch (Exception e)
                {
                    if (!IsReprocessable(e, ref count))
                        return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// The <paramref name="result"/> is set to a result of the <paramref name="execute"/> after successfull execution or to <c>default(T)</c> after exception.
        /// Returns <c>true</c> if execution was successfull (within the max count of re-run); <c>false</c> otherwise.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <param name="result">A result from the <paramref name="execute"/>.</param>
        /// <returns><c>true</c> if execution was successfull (within the max count of re-run); <c>false</c> otherwise.</returns>
        public bool Run<T>(Func<T> execute, out T result)
        {
            Ensure.NotNull(execute, "execute");

            int count = 0;
            while (count < maxCount)
            {
                try
                {
                    result = execute();
                    return true;
                }
                catch (Exception e)
                {
                    if (!IsReprocessable(e, ref count))
                    {
                        result = default;
                        return false;
                    }
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// Returns result from the <paramref name="execute"/> or <c>default(T)</c> after exception.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <returns>Result from the <paramref name="execute"/> or <c>default(T)</c> after exception.</returns>
        public T Run<T>(Func<T> execute)
        {
            Ensure.NotNull(execute, "execute");

            int count = 0;
            while (count < maxCount)
            {
                try
                {
                    return execute();
                }
                catch (Exception e)
                {
                    if (!IsReprocessable(e, ref count))
                        return default;
                }
            }

            return default;
        }
    }
}
