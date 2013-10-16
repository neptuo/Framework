using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public abstract class ModelValidatorBase : IModelValidator
    {
        protected IModelDefinition ModelDefinition { get; private set; }

        public ModelValidatorBase(IModelDefinition modelDefinition)
        {
            if (modelDefinition == null)
                throw new ArgumentNullException("modelDefinition");

            ModelDefinition = modelDefinition;
        }

        protected virtual ModelValidationBuilder CreateResultBuilder()
        {
            return new ModelValidationBuilder();
        }

        public IModelValidationResult Validate(IModelValueGetter getter)
        {
            IModelValidationBuilder resultBuilder = CreateResultBuilder();
            foreach (IFieldDefinition fieldDefinition in ModelDefinition.Fields)
                ValidateField(fieldDefinition, getter, resultBuilder);

            return resultBuilder.ToResult();
        }

        protected abstract void ValidateField(IFieldDefinition fieldDefinition, IModelValueGetter getter, IModelValidationBuilder resultBuilder);
    }
}
