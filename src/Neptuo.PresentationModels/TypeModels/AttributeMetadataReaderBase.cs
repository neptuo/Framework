using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    /// <summary>
    /// Base implementation of <see cref="IAttributeMetadataReader"/> for specific type of attribute.
    /// </summary>
    /// <typeparam name="T">Type of attribute to process.</typeparam>
    public abstract class AttributeMetadataReaderBase<T> : IAttributeMetadataReader
        where T : Attribute
    {
        public void Apply(Attribute attribute, IMetadataBuilder builder)
        {
            Ensure.NotNull(attribute, "attribute");
            Ensure.NotNull(builder, "builder");

            T targetAttribute = attribute as T;
            if (targetAttribute == null)
                throw Ensure.Exception.InvalidOperation("Reader can process only attribute of type '{0}'", typeof(T).FullName);

            ApplyInternal(targetAttribute, builder);
        }

        /// <summary>
        /// Process attribute usage.
        /// </summary>
        /// <param name="attribute">Attribute instance.</param>
        /// <param name="builder">Metadata builder.</param>
        protected abstract void ApplyInternal(T attribute, IMetadataBuilder builder);
    }
}
