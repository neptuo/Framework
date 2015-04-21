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
    /// <c>Display.Name</c>
    /// <c>Display.ShortName</c>
    /// <c>Description</c>
    /// <c>Display.Order</c>
    /// <c>Display.Watermark</c>
    /// </summary>
    public class DisplayMetadataReader : AttributeMetadataReaderBase<DisplayAttribute>
    {
        protected override void ApplyInternal(DisplayAttribute attribute, IMetadataBuilder builder)
        {
            builder.Add("Display.Name", attribute.Name);
            builder.Add("Display.ShortName", attribute.ShortName);

            if (!builder.Has("Description"))
                builder.Add("Description", attribute.Description);

            builder.Add("Display.Order", attribute.Order);
            builder.Add("Display.Watermark", attribute.Prompt);
        }
    }
}
