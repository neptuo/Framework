using Neptuo.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Converters
{
    /// <summary>
    /// Composite converters extensions for <see cref="IConverterRepository"/>.
    /// </summary>
    public static class _ConverterRepositoryExtensions
    {
        /// <summary>
        /// Adds <see cref="CompositeConverterCollection"/> to the <paramref name="repository"/>.
        /// </summary>
        /// <param name="repository">The repository to add converter to.</param>
        /// <returns>The instance of the converter collection.</returns>
        public static CompositeConverterCollection AddComposite(this IConverterRepository repository)
        {
            Ensure.NotNull(repository, "repository");

            CompositeConverterCollection converter = new CompositeConverterCollection();
            repository
                .Add(typeof(CompositeDeserializerContext), typeof(object), converter)
                .Add(typeof(CompositeSerializerContext), typeof(bool), converter);

            return converter;
        }
    }
}
