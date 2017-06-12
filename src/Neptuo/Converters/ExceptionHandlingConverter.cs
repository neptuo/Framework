using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    /// <summary>
    /// A implementation of <see cref="IConverter{TSource, TTarget}"/> that uses passed delegate for every raised exceptions.
    /// This exception handling delegate can log the exception, sink it or rethrow it.
    /// </summary>
    /// <typeparam name="TSource">A type of the source value.</typeparam>
    /// <typeparam name="TTarget">A type of the target value.</typeparam>
    public class ExceptionHandlingConverter<TSource, TTarget> : ExceptionHandlingConverter, IConverter<TSource, TTarget>
    {
        private readonly IConverter<TSource, TTarget> converter;
        private readonly Action<Exception> exceptionHandler;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="converter">An inner converter.</param>
        /// <param name="exceptionHandler">An exception handling delegate.</param>
        public ExceptionHandlingConverter(IConverter<TSource, TTarget> converter, Action<Exception> exceptionHandler)
            : base(converter, exceptionHandler)
        {
            Ensure.NotNull(converter, "converter");
            Ensure.NotNull(exceptionHandler, "exceptionHandler");
            this.converter = converter;
            this.exceptionHandler = exceptionHandler;
        }

        public bool TryConvert(TSource sourceValue, out TTarget targetValue)
        {
            try
            {
                return converter.TryConvert(sourceValue, out targetValue);
            }
            catch (Exception e)
            {
                exceptionHandler(e);
            }

            targetValue = default(TTarget);
            return false;
        }
    }

    /// <summary>
    /// A implementation of <see cref="IConverter"/> that uses passed delegate for every raised exceptions.
    /// This exception handling delegate can log the exception, sink it or rethrow it.
    /// </summary>
    public class ExceptionHandlingConverter : IConverter
    {
        private readonly IConverter converter;
        private readonly Action<Exception> exceptionHandler;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="converter">An inner converter.</param>
        /// <param name="exceptionHandler">An exception handling delegate.</param>
        public ExceptionHandlingConverter(IConverter converter, Action<Exception> exceptionHandler)
        {
            Ensure.NotNull(converter, "converter");
            Ensure.NotNull(exceptionHandler, "exceptionHandler");
            this.converter = converter;
            this.exceptionHandler = exceptionHandler;
        }

        public bool TryConvert(Type sourceType, Type targetType, object sourceValue, out object targetValue)
        {
            try
            {
                return converter.TryConvert(sourceType, targetType, sourceValue, out targetValue);
            }
            catch (Exception e)
            {
                exceptionHandler(e);
            }

            targetValue = null;
            return false;
        }
    }
}
