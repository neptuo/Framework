using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public class MetadataValidatorCollection : IMetadataValidatorCollection
    {
        protected Dictionary<string, Dictionary<string, Dictionary<string, IFieldMetadataValidatorFactory>>> Validators { get; private set; }

        public MetadataValidatorCollection()
        {
            Validators = new Dictionary<string, Dictionary<string, Dictionary<string, IFieldMetadataValidatorFactory>>>();
        }

        public void Add(string modelIdentifier, string fieldIdentifier, string metadataKey, IFieldMetadataValidatorFactory validatorFactory)
        {
            if (modelIdentifier == null)
                modelIdentifier = String.Empty;

            if (fieldIdentifier == null)
                fieldIdentifier = String.Empty;

            if (!Validators.ContainsKey(modelIdentifier))
                Validators[modelIdentifier] = new Dictionary<string, Dictionary<string, IFieldMetadataValidatorFactory>>();

            if (!Validators[modelIdentifier].ContainsKey(fieldIdentifier))
                Validators[modelIdentifier][fieldIdentifier] = new Dictionary<string, IFieldMetadataValidatorFactory>();

            Validators[modelIdentifier][fieldIdentifier][metadataKey] = validatorFactory;
        }

        public bool TryGet(string modelIdentifier, string fieldIdentifier, string metadataKey, out IFieldMetadataValidator validator)
        {
            Dictionary<string, Dictionary<string, IFieldMetadataValidatorFactory>> modelValidators;
            if (!Validators.TryGetValue(modelIdentifier, out modelValidators) && !Validators.TryGetValue(String.Empty, out modelValidators))
            {
                validator = null;
                return false;
            }

            Dictionary<string, IFieldMetadataValidatorFactory> fieldValidators;
            if (!modelValidators.TryGetValue(fieldIdentifier, out fieldValidators) && !modelValidators.TryGetValue(String.Empty, out fieldValidators))
            {
                validator = null;
                return false;
            }

            IFieldMetadataValidatorFactory factory;
            if (!fieldValidators.TryGetValue(metadataKey, out factory) && !fieldValidators.TryGetValue(String.Empty, out factory))
            {
                validator = null;
                return false;
            }

            validator = factory.Create();
            return true;
        }
    }
}
