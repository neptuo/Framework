using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public interface IModelDefinition
    {
        string Identifier { get; }
        IEnumerable<IFieldDefinition> Fields { get; }
        IModelMetadataCollection Metadata { get; }
    }
}
