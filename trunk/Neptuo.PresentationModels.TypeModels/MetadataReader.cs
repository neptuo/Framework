using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    public abstract class MetadataReader<T> : IMetadataReader
        where T : Attribute
    {
        public void Apply(Attribute attribute, IMetadataBuilder builder)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");

            if (builder == null)
                throw new ArgumentNullException("builder");

            T targetAttribute = attribute as T;
            if (targetAttribute == null)
                throw new InvalidOperationException(String.Format("Reader can process only attribute of type '{0}'", typeof(T).FullName));

            ApplyInternal(targetAttribute, builder);
        }

        protected abstract void ApplyInternal(T attribute, IMetadataBuilder builder);
    }
}
