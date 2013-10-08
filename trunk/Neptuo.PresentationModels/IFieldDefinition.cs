using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public interface IFieldDefinition
    {
        string Identifier { get; }
        Type FieldType { get; }
        IFieldMetadataCollection Metadata { get; }
    }
}
