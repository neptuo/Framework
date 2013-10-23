using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public class SingletonFieldMetadataValidatorFactory : IFieldMetadataValidatorFactory
    {
        public IFieldMetadataValidator Validator { get; private set; }

        public SingletonFieldMetadataValidatorFactory(IFieldMetadataValidator validator)
        {
            if (validator == null)
                throw new ArgumentNullException("validator");

            Validator = validator;
        }

        public IFieldMetadataValidator Create()
        {
            return Validator;
        }
    }
}
