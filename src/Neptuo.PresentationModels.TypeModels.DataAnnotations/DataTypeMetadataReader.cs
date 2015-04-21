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
    /// <c>DataType</c>
    /// </summary>
    public class DataTypeMetadataReader : AttributeMetadataReaderBase<DataTypeAttribute>
    {
        protected override void ApplyInternal(DataTypeAttribute attribute, IMetadataBuilder builder)
        {
            builder.Add("DataType", attribute.DataType);
        }
    }
}
