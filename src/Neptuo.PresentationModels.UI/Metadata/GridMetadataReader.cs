using Neptuo.PresentationModels.TypeModels;
using Neptuo.PresentationModels.UI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI.Metadata
{
    /// <summary>
    /// Provides keys:
    /// <c>GridColumnIndex</c>
    /// <c>GridColumnSpan</c>
    /// <c>GridRowIndex</c>
    /// <c>GridRowSpan</c>
    /// </summary>
    public class GridMetadataReader : AttributeMetadataReaderBase<GridAttribute>
    {
        protected override void ApplyInternal(GridAttribute attribute, IMetadataBuilder builder)
        {
            builder
                .AddGridColumnIndex(attribute.ColumnIndex)
                .AddGridColumnSpan(attribute.ColumnSpan)
                .AddGridRowIndex(attribute.RowIndex)
                .AddGridRowSpan(attribute.RowSpan);
        }
    }
}
