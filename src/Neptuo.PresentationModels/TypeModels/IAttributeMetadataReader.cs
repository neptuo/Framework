using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    /// <summary>
    /// Attribute based metadata builder.
    /// </summary>
    public interface IAttributeMetadataReader
    {
        /// <summary>
        /// Process attribute usage.
        /// </summary>
        /// <param name="attribute">Attribute instance.</param>
        /// <param name="builder">Metadata builder.</param>
        void Apply(Attribute attribute, IMetadataBuilder builder);
    }
}
