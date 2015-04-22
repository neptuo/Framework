using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    /// <summary>
    /// Context-less metadata reader.
    /// </summary>
    public interface IMetadataReader
    {
        /// <summary>
        /// Appends metadata to the <paramref name="builder"/>.
        /// </summary>
        /// <param name="builder">Metadata builder.</param>
        IAttributeMetadataReader Apply(IMetadataBuilder builder);
    }
}
