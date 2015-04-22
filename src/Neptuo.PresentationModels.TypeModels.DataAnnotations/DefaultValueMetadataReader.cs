using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.DataAnnotations
{
    /// <summary>
    /// Provides keys:
    /// <c>DefaultValue</c>
    /// </summary>
    public class DefaultValueMetadataReader : AttributeMetadataReaderBase<DefaultValueAttribute>
    {
        protected override void ApplyInternal(DefaultValueAttribute attribute, IMetadataBuilder builder)
        {
            builder.Add("DefaultValue", attribute.Value);
        }
    }
}
