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
    /// <c>StringLength.</c>
    /// <c>StringLength.Minimum</c>
    /// <c>StringLength.Maximum</c>
    /// </summary>
    public class StringLengthMetadataReader : AttributeMetadataReaderBase<StringLengthAttribute>
    {
        protected override void ApplyInternal(StringLengthAttribute attribute, IMetadataBuilder builder)
        {
            builder.Add("StringLength.ErrorMessage", attribute.ErrorMessage);
            builder.Add("StringLength.Minimum", attribute.MinimumLength);
            builder.Add("StringLength.Maximum", attribute.MaximumLength);
        }
    }
}
