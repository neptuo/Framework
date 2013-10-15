using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public class MetadataValidatorCollection : IMetadataValidatorCollection
    {
        protected Dictionary<string, Dictionary<string, IFiedMetadataValidator>> Validators { get; private set; }

        public MetadataValidatorCollection()
        {
            Validators = new Dictionary<string, Dictionary<string, IFiedMetadataValidator>>();
        }

        public void Add(string modelIdentifier, string fieldIdentifier, string metadataKey, IFiedMetadataValidatorFactory validatorFactory)
        {
            if (modelIdentifier == null)
                modelIdentifier = String.Empty;

            throw new NotImplementedException();
        }

        public bool TryGet(string modelIdentifier, string fieldIdentifier, string metadataKey, out IFiedMetadataValidator validator)
        {
            throw new NotImplementedException();
        }
    }
}
