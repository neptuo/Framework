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
    /// <c>Description</c>
    /// </summary>
    public class DescriptionMetadataReader : AttributeMetadataReaderBase<DescriptionAttribute>
    {
        protected override void ApplyInternal(DescriptionAttribute attribute, IMetadataBuilder builder)
        {
            if (!builder.Has("Description"))
                builder.Add("Description", attribute.Description);
        }
    }
}
