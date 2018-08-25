using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions
{
    /// <summary>
    /// The exception-safe runner for actions and functions.
    /// </summary>
    public interface ITryCatch
    {
        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// Returns <c>true</c> if execution was successfull (without exception); <c>false</c> otherwise.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <returns><c>true</c> if execution was successfull (without exception); <c>false</c> otherwise.</returns>
        bool Run(Action execute);

        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// Returns <c>true</c> if execution was successfull (without exception); <c>false</c> otherwise.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <returns><c>true</c> if execution was successfull (without exception); <c>false</c> otherwise.</returns>
        Task<bool> RunAsync(Func<Task> execute);

        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// The <paramref name="result"/> is set to a result of the <paramref name="execute"/> after successfull execution or to <c>default(T)</c> after exception.
        /// Returns <c>true</c> if execution was successfull (without exception); <c>false</c> otherwise.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <param name="result">A result from the <paramref name="execute"/>.</param>
        /// <returns><c>true</c> if execution was successfull (without exception); <c>false</c> otherwise.</returns>
        bool Run<T>(Func<T> execute, out T result);

        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// Returns result from the <paramref name="execute"/> or <c>default(T)</c> after exception.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <returns>Result from the <paramref name="execute"/> or <c>default(T)</c> after exception.</returns>
        T Run<T>(Func<T> execute);

        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// Returns result from the <paramref name="execute"/> or <c>default(T)</c> after exception.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <returns>Result from the <paramref name="execute"/> or <c>default(T)</c> after exception.</returns>
        Task<T> RunAsync<T>(Func<Task<T>> execute);
    }
}
