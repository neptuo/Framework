﻿using System;
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
        /// <param name="converter">The converter delegate.</param>
        /// <returns><paramref name="repository"/>.</returns>
        public static IConverterRepository Add<TSource, TTarget>(this IConverterRepository repository, OutFunc<TSource, TTarget, bool> tryConvert)
        {
            Ensure.NotNull(repository, "repository");
            Ensure.NotNull(tryConvert, "tryConvert");
            return Add(repository, new ConverterBase<TSource, TTarget>(tryConvert));
        }
    }
}
