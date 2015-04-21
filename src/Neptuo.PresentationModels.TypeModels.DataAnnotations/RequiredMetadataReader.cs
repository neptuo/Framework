using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.DataAnnotations
{
    /// <summary>
    /// Provides keys:
    /// <c>Required</c>
    /// <c>Required.AllowEmptyStrings</c>
    /// <c>Required.ErrorMessage</c>
    /// <c>Required.Attribute</c>
    /// </summary>
    public class RequiredMetadataReader : AttributeMetadataReaderBase<RequiredAttribute>
    {
        protected override void ApplyInternal(RequiredAttribute attribute, IMetadataBuilder builder)
        {
            builder.Add("Required", true);
            builder.Add("Required.AllowEmptyStrings", attribute.AllowEmptyStrings);
            builder.Add("Required.ErrorMessage", attribute.ErrorMessage);
            builder.Add("Required.Attribute", attribute);
        }
    }
}
