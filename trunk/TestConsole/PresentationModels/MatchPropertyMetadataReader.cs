using Neptuo.PresentationModels.TypeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.PresentationModels
{
    public class MatchPropertyMetadataReader : MetadataReaderBase<MatchPropertyAttribute>
    {
        protected override void ApplyInternal(MatchPropertyAttribute attribute, IMetadataBuilder builder)
        {
            builder.Set("MatchProperty", attribute.TargetProperty);
        }
    }
}
