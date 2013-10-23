using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.DataAnnotations
{
    public class DescriptionMetadataReader : MetadataReaderBase<DescriptionAttribute>
    {
        protected override void ApplyInternal(DescriptionAttribute attribute, IMetadataBuilder builder)
        {
            builder.Set("Description", attribute.Description);
        }
    }
}
