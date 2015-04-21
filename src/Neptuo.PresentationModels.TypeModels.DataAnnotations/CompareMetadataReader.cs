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
    /// <c>MatchProperty</c>
    /// <c>MatchProperty.ErrorMessage</c>
    /// <c>MatchProperty.Attribute</c>
    /// </summary>
    public class CompareMetadataReader : AttributeMetadataReaderBase<CompareAttribute>
    {
        protected override void ApplyInternal(CompareAttribute attribute, IMetadataBuilder builder)
        {
            builder.Add("MatchProperty", attribute.OtherProperty);
            builder.Add("MatchProperty.ErrorMessage", attribute.ErrorMessage);
            builder.Add("MatchProperty.Attribute", attribute);
        }
    }
}
