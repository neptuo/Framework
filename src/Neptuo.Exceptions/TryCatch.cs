using Neptuo;
using Neptuo.Exceptions.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions
{
    /// <summary>
    /// The exception-safe runner for actions and functions.
    /// </summary>
    public class TryCatch
    {
        private readonly IExceptionHandler handler;

        /// <summary>
        /// Creates a new instance that uses <paramref name="handler"/> to handle exceptions.
        /// If the <paramref name="handler"/> throws an exception, this exception is not handled.
        /// </summary>
        /// <param name="handler">An exception handler.</param>
        public TryCatch(IExceptionHandler handler)
        {
            Ensure.NotNull(handler, "handler");
            this.handler = handler;
        }

        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// Returns <c>true</c> if execution was successfull (without exception); <c>false</c> otherwise.
        /// Note: If the <see cref="IExceptionHandler"/> throws an exception, this exception is not handled.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <returns><c>true</c> if execution was successfull (without exception); <c>false</c> otherwise.</returns>
        public bool Run(Action execute)
        {
            Ensure.NotNull(execute, "execute");
            try
            {
                execute();
                return true;
            }
            catch (Exception e)
            {
                handler.Handle(e);
                return false;
            }
        }

        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// The <paramref name="result"/> is set a result of the <paramref name="execute"/> after successfull execution or to <c>default(T)</c> after exception.
        /// Returns <c>true</c> if execution was successfull (without exception); <c>false</c> otherwise.
        /// Note: If the <see cref="IExceptionHandler"/> throws an exception, this exception is not handled.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <param name="result">A result from the <paramref name="execute"/>.</param>
        /// <returns><c>true</c> if execution was successfull (without exception); <c>false</c> otherwise.</returns>
        public bool Run<T>(Func<T> execute, out T result)
        {
            Ensure.NotNull(execute, "execute");
            try
            {
                result = execute();
                return true;
            }
            catch (Exception e)
            {
                handler.Handle(e);
                result = default(T);
                return false;
            }
        }

        /// <summary>
        /// Wraps execution of an <paramref name="execute"/> with try-catch.
        /// Returns result from the <paramref name="execute"/> or <c>default(T)</c> after exception.
        /// Note: If the <see cref="IExceptionHandler"/> throws an exception, this exception is not handled.
        /// </summary>
        /// <param name="execute">An action to execute.</param>
        /// <returns>Result from the <paramref name="execute"/> or <c>default(T)</c> after exception.</returns>
        public T Run<T>(Func<T> execute)
        {
            Ensure.NotNull(execute, "execute");
            try
            {
                return execute();
            }
            catch (Exception e)
            {
                handler.Handle(e);
                return default(T);
            }
        }


        /// <summary>
        /// Wraps <paramref name="execute"/> with a delegate containing try-catch block.
        /// Note: If the <see cref="IExceptionHandler"/> throws an exception, this exception is not handled.
        /// </summary>
        /// <typeparam name="T">A type of a desired delegate.</typeparam>
        /// <param name="execute">An delegate to wrap.</param>
        /// <returns>Wrapped <paramref name="execute"/> with a delegate containing try-catch block.</returns>
        public T Wrap<T>(T execute)
        {
            Ensure.NotNull(execute, "execute");

            Delegate source = execute as Delegate;
            if (source == null)
                throw Ensure.Exception.ArgumentOutOfRange("execute", "The 'execute' parameter must be an instance of a delegate.");

            MethodInfo method = source.Method;

            ParameterExpression[] parameters = MapParameters(method);
            ParameterExpression exception = Expression.Parameter(
                typeof(Exception),
                "e"
            );

            Expression body = Expression.TryCatch(
                Expression.Invoke(
                    Expression.Constant(execute),
                    parameters
                ),
                Expression.Catch(
                    exception,
                    Expression.Call(
                        Expression.Constant(handler, typeof(IExceptionHandler)),
                        nameof(IExceptionHandler.Handle),
                        null,
                        exception
                    )
                )
            );

            LambdaExpression lambda = Expression.Lambda(typeof(T), body, parameters);
            Delegate target = lambda.Compile();
            return (T)(object)target;
        }

        private ParameterExpression[] MapParameters(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            ParameterExpression[] result = new ParameterExpression[parameters.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = Expression.Parameter(parameters[i].ParameterType, parameters[i].Name);

            return result;
        }
    }
}
