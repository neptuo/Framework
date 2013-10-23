using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public class FuncFieldMetadataValidatorFactory<TValidator> : IFieldMetadataValidatorFactory
        where TValidator : IFieldMetadataValidator
    {
        public Func<TValidator> Factory { get; private set; }

        public FuncFieldMetadataValidatorFactory(Func<TValidator> factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            Factory = factory;
        }

        public IFieldMetadataValidator Create()
        {
            return Factory();
        }
    }

    public class FuncFieldMetadataValidatorFactory : FuncFieldMetadataValidatorFactory<IFieldMetadataValidator>
    {
        public FuncFieldMetadataValidatorFactory(Func<IFieldMetadataValidator> factory)
            : base(factory)
        { }
    }
}
