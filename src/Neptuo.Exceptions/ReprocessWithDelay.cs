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
    public class ReprocessWithDelay
    {
        private readonly IExceptionHandler<IExceptionHandlerContext> handler;
        private readonly IExceptionHandler greedyHandler;
        private readonly int maxCount;
        private readonly TimeSpan delay;

        /// <summary>
        /// Creates a new instance that uses <paramref name="handler"/> to pass all exceptions to.
        /// All exceptions are reprocessable.
        /// </summary>
        /// <param name="handler">An exception handler.</param>
        /// <param name="maxCount">A maximum count of re-run. Must be positive number.</param>
        /// <param name="delay">A delay to take between reprocesses.</param>
        public ReprocessWithDelay(IExceptionHandler handler, int maxCount, TimeSpan delay)
        {
            Ensure.NotNull(handler, "handler");
            Ensure.Positive(maxCount, "maxCount");
            Ensure.PositiveOrZero(delay.TotalMilliseconds, "delay");
            this.greedyHandler = handler;
            this.maxCount = maxCount;
            this.delay = delay;
        }

        /// <summary>
        /// Creates a new instance that uses <paramref name="handler"/> to determine, which exceptions are reprocessable.
        /// If the <paramref name="handler"/> marks the exception as handled, the <see cref="ReprocessWithDelay"/> tries to run the action again; otherwise immediately stops the execetion of action/function.
        /// </summary>
        /// <param name="handler">An exception handler.</param>
        /// <param name="maxCount">A maximum count of re-run. Must be positive number.</param>
        /// <param name="delay">A delay to take between reprocesses.</param>
        public ReprocessWithDelay(IExceptionHandler<IExceptionHandlerContext> handler, int maxCount, TimeSpan delay)
        {
            Ensure.NotNull(handler, "handler");
            Ensure.Positive(maxCount, "maxCount");
            Ensure.PositiveOrZero(delay.TotalMilliseconds, "delay");
            this.handler = handler;
            this.maxCount = maxCount;
            this.delay = delay;
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
        /// Between every reprocess, a delay is taken.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <returns><c>true</c> if execution was successfull (within the max count of re-run); <c>false</c> otherwise.</returns>
        public async Task<bool> RunAsync(Action execute)
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

                if (count < maxCount)
                    await Task.Delay(delay);
            }

            return false;
        }

        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// Returns <c>true</c> if execution was successfull (within the max count of re-run); <c>false</c> otherwise.
        /// Between every reprocess, a delay is taken.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <returns><c>true</c> if execution was successfull (within the max count of re-run); <c>false</c> otherwise.</returns>
        public async Task<bool> RunAsync(Func<Task> execute)
        {
            Ensure.NotNull(execute, "execute");

            int count = 0;
            while (count < maxCount)
            {
                try
                {
                    await execute();
                    return true;
                }
                catch (Exception e)
                {
                    if (!IsReprocessable(e, ref count))
                        return false;
                }

                if (count < maxCount)
                    await Task.Delay(delay);
            }

            return false;
        }

        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// Returns result from the <paramref name="execute"/> (within the max count of re-run) or <c>default(T)</c> after exception.
        /// Between every reprocess, a delay is taken.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <returns>Result from the <paramref name="execute"/> or <c>default(T)</c> after exception.</returns>
        public async Task<T> RunAsync<T>(Func<T> execute)
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

                if (count < maxCount)
                    await Task.Delay(delay);
            }

            return default;
        }

        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// Returns result from the <paramref name="execute"/> (within the max count of re-run) or <c>default(T)</c> after exception.
        /// Between every reprocess, a delay is taken.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <returns>Result from the <paramref name="execute"/> or <c>default(T)</c> after exception.</returns>
        public async Task<T> RunAsync<T>(Func<Task<T>> execute)
        {
            Ensure.NotNull(execute, "execute");

            int count = 0;
            while (count < maxCount)
            {
                try
                {
                    return await execute();
                }
                catch (Exception e)
                {
                    if (!IsReprocessable(e, ref count))
                        return default;
                }

                if (count < maxCount)
                    await Task.Delay(delay);
            }

            return default;
        }
    }
}
