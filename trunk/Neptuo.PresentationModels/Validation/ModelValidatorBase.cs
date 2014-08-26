using Neptuo.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    /// <summary>
    /// Base for <see cref="IModelValidator"/>.
    /// Validator result built from validation of all fields.
    /// </summary>
    public abstract class ModelValidatorBase : IModelValidator
    {
        /// <summary>
        /// Model definition to validate.
        /// </summary>
        protected IModelDefinition ModelDefinition { get; private set; }

        public ModelValidatorBase(IModelDefinition modelDefinition)
        {
            Guard.NotNull(modelDefinition, "modelDefinition");
            ModelDefinition = modelDefinition;
        }

        /// <summary>
        /// Creates instance of validation result builder.
        /// </summary>
        /// <returns></returns>
        protected virtual IModelValidationBuilder CreateResultBuilder()
        {
            return new ModelValidationBuilder();
        }

        public virtual IValidationResult Validate(IModelValueGetter getter)
        {
            IModelValidationBuilder resultBuilder = CreateResultBuilder();
            foreach (IFieldDefinition fieldDefinition in ModelDefinition.Fields)
                ValidateField(fieldDefinition, getter, resultBuilder);

            return resultBuilder.ToResult();
        }

        /// <summary>
        /// Provides logic for validating <paramref name="fieldDefinition"/>.
        /// </summary>
        /// <param name="fieldDefinition">Defines field to validate.</param>
        /// <param name="getter">Provides current values.</param>
        /// <param name="resultBuilder">Validation result builder.</param>
        protected abstract void ValidateField(IFieldDefinition fieldDefinition, IModelValueGetter getter, IModelValidationBuilder resultBuilder);
    }
}
