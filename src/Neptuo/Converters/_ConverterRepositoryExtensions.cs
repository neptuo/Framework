using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    /// <summary>
    /// Registration extensions for <see cref="IConverterRepository"/>.
    /// </summary>
    public static class _ConverterRepositoryExtensions
    {
        /// <summary>
        /// Adds <paramref name="converter"/> for conversion from <typeparamref name="TSource"/> to <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TTarget">Target type.</typeparam>
        /// <param name="repository">The repository to register converter to.</param>
        /// <param name="converter">The converter.</param>
        /// <returns><paramref name="repository"/>.</returns>
        public static IConverterRepository Add<TSource, TTarget>(this IConverterRepository repository, IConverter<TSource, TTarget> converter)
        {
            Ensure.NotNull(repository, "repository");
            repository.Add(typeof(TSource), typeof(TTarget), converter);
            return repository;
        }

        /// <summary>
        /// Adds <paramref name="tryConvert"/> for conversion from <typeparamref name="TSource"/> to <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TTarget">Target type.</typeparam>
        /// <param name="repository">The repository to register converter to.</param>
        /// <param name="tryConvert">The converter delegate.</param>
        /// <returns><paramref name="repository"/>.</returns>
        public static IConverterRepository Add<TSource, TTarget>(this IConverterRepository repository, OutFunc<TSource, TTarget, bool> tryConvert)
        {
            Ensure.NotNull(repository, "repository");
            Ensure.NotNull(tryConvert, "tryConvert");
            return Add(repository, new ConverterBase<TSource, TTarget>(tryConvert));
        }

        /// <summary>
        /// Adds <paramref name="tryConvert"/> for conversion from <see cref="String"/> to <typeparamref name="TTarget"/>.
        /// Alse adds support for nullable conversion from <see cref="String"/> to <see cref="Nullable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TTarget">Target type.</typeparam>
        /// <param name="repository">The repository to register converter to.</param>
        /// <param name="tryConvert">The converter delegate.</param>
        /// <param name="isWhitespaceAccepted">Whether whitespaces should be treated as nulls.</param>
        /// <returns><paramref name="repository"/>.</returns>
        public static IConverterRepository AddStringTo<TTarget>(this IConverterRepository repository, OutFunc<string, TTarget, bool> tryConvert, bool isWhitespaceAccepted = true)
            where TTarget : struct
        {
            Ensure.NotNull(repository, "repository");
            Ensure.NotNull(tryConvert, "tryConvert");

            IConverter<string, TTarget> converter = new ConverterBase<string, TTarget>(tryConvert);
            return repository
                .Add<string, TTarget>(converter)
                .Add<string, TTarget?>(new StringToNullableConverter<TTarget>(converter, isWhitespaceAccepted));
        }

        /// <summary>
        /// Adds converter for conversion from <typeparamref name="TSource"/> to <see cref="String"/>.
        /// If <paramref name="format"/> is not <c>null</c>, then it is used as format string, eg. yyyy-MM-dd.
        /// </summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <param name="repository">The repository to register converter to.</param>
        /// <param name="format">Format string, eg. yyyy-MM-dd for datetime.</param>
        /// <returns><paramref name="repository"/>.</returns>
        public static IConverterRepository AddToString<TSource>(this IConverterRepository repository, string format = null)
        {
            Ensure.NotNull(repository, "repository");
            if (format == null)
                return Add(repository, new ToStringConverter<TSource>());
            else
                return Add(repository, new ToStringConverter<TSource>(format));
        }

        /// <summary>
        /// Adds search handler for converting any type to string.
        /// </summary>
        /// <param name="repository">The repository to register converter to.</param>
        /// <returns><paramref name="repository"/>.</returns>
        public static IConverterRepository AddToStringSearchHandler(this IConverterRepository repository)
        {
            Ensure.NotNull(repository, "repository");
            return repository.AddSearchHandler(TryConvertToString);
        }

        private static bool TryConvertToString(ConverterSearchContext context, out IConverter converter)
        {
            converter = new ToStringConverter();
            return true;
        }

        /// <summary>
        /// Adds string to any enum (and any nullable enum) converter search handler.
        /// Any enums types will be converted using <see cref="StringToEnumConverter"/>.
        /// </summary>
        /// <param name="repository">The repository to register converter to.</param>
        /// <param name="isCaseSensitive">Whether parsing should be case-sensitive.</param>
        /// <returns><paramref name="repository"/>.</returns>
        public static IConverterRepository AddEnumSearchHandler(this IConverterRepository repository, bool isCaseSensitive)
        {
            Ensure.NotNull(repository, "repository");

            if (isCaseSensitive)
                return repository.AddSearchHandler(TryConvertStringToEnumCaseSensitive);
            else
                return repository.AddSearchHandler(TryConvertStringToEnum);
        }

        private static bool TryConvertStringToEnumCaseSensitive(ConverterSearchContext context, out IConverter converter)
        {
            if (context.TargetType.IsEnum)
            {
                converter = new StringToEnumConverter(true);
                return true;
            }
            else if (context.TargetType.IsGenericType && context.TargetType.GetGenericTypeDefinition() == typeof(Nullable<>) && context.TargetType.GetGenericArguments()[0].IsEnum)
            {
                converter = new StringToNullableConverter(new StringToEnumConverter(false), false);
                return true;
            }

            converter = null;
            return false;
        }

        private static bool TryConvertStringToEnum(ConverterSearchContext context, out IConverter converter)
        {
            if (context.TargetType.IsEnum)
            {
                converter = new StringToEnumConverter(false);
                return true;
            }
            else if (context.TargetType.IsGenericType && context.TargetType.GetGenericTypeDefinition() == typeof(Nullable<>) && context.TargetType.GetGenericArguments()[0].IsEnum)
            {
                converter = new StringToNullableConverter(new StringToEnumConverter(false), true);
                return true;
            }

            converter = null;
            return false;
        }
    }
}
