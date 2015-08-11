using Neptuo.Services.Validators;
using Neptuo.Services.Validators.Handlers;
using Neptuo.PresentationModels.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators.Handlers
{
    /// <summary>
    /// Base for <see cref="IModelValidationHandler"/>.
    /// Validator result built from validation of all fields.
    /// </summary>
    public class ModelValidator : IValidationHandler<ModelValidatorContext>
    {
        /// <summary>
        /// Field validator.
        /// </summary>
        protected IFieldValidator FieldValidator { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="fieldValidator"/> to validating fields.
        /// </summary>
        /// <param name="fieldValidator">Field validator.</param>
        public ModelValidator(IFieldValidator fieldValidator)
        {
            Ensure.NotNull(fieldValidator, "fieldValidator");
            FieldValidator = fieldValidator;
        }

        /// <summary>
        /// Creates instance of validation result builder.
        /// </summary>
        /// <returns></returns>
        protected virtual IValidationResultBuilder CreateResultBuilder()
        {
            return new ValidationResultBuilder(true);
        }

        public Task<IValidationResult> HandleAsync(ModelValidatorContext context)
        {
            IValidationResultBuilder resultBuilder = CreateResultBuilder();
            ValidateInternal(context.Definition, context.Getter, resultBuilder);
            return Task.FromResult(resultBuilder.ToResult());
        }

        /// <summary>
        /// Provides logic for validating model.
        /// </summary>
        /// <param name="modelDefinition">Model definition.</param>
        /// <param name="fieldDefinition">Defines field to validate.</param>
        /// <param name="getter">Provides current values.</param>
        /// <param name="resultBuilder">Validation result builder.</param>
        protected virtual void ValidateInternal(IModelDefinition modelDefinition, IModelValueGetter getter, IValidationResultBuilder resultBuilder)
        {
            foreach (IFieldDefinition fieldDefinition in modelDefinition.Fields)
                FieldValidator.Validate(modelDefinition, fieldDefinition, getter, resultBuilder);
        }
    }
}
