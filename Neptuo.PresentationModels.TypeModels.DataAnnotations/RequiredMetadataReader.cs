using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.DataAnnotations
{
    public class RequiredMetadataReader : MetadataReaderBase<RequiredAttribute>
    {
        protected override void ApplyInternal(RequiredAttribute attribute, IMetadataBuilder builder)
        {
            builder.Set("Required", true);
            builder.Set("Required.AllowEmptyStrings", attribute.AllowEmptyStrings);
            builder.Set("Required.ErrorMessage", attribute.ErrorMessage);
        }
    }
}
