using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters.AutoExports
{
    /// <summary>
    /// Auto converter wiring.
    /// Supports two scenarios:
    /// 1) Registers all implemented converters, works only for implementations of <see cref="IConverter{TSource, TTarget}"/>.
    ///    => Use parameterless constructor.
    /// 2) Registers only specified conversions.
    ///    => Use constructor with two parameters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ConverterAttribute : Attribute
    {
        /// <summary>
        /// Conversion source type.
        /// </summary>
        public Type SourceType { get; private set; }

        /// <summary>
        /// Conversion target type.
        /// </summary>
        public Type TargetType { get; private set; }

        /// <summary>
        /// Whether is general use or type-specific usage.
        /// </summary>
        public bool HasTypesDefined { get; private set; }

        /// <summary>
        /// Creates a new empty instance.
        /// </summary>
        public ConverterAttribute()
        { }

        /// <summary>
        /// Creates a new instance with defined source and target type.
        /// </summary>
        /// <param name="sourceType">A conversion source type.</param>
        /// <param name="targetType">A conversion target type.</param>
        public ConverterAttribute(Type sourceType, Type targetType)
        {
            Ensure.NotNull(sourceType, "sourceType");
            Ensure.NotNull(targetType, "targetType");
            SourceType = sourceType;
            TargetType = targetType;
            HasTypesDefined = true;
        }
    }
}
