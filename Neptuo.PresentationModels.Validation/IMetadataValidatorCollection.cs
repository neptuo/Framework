using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public interface IMetadataValidatorCollection
    {
        bool TryGet(string modelIdentifier, string fieldIdentifier, string metadataKey, out IFiedMetadataValidator validator);
    }
}
