using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    /// <summary>
    /// Description of model validator for <see cref="Pipelines.Validators.IValidationDispatcher"/>.
    /// </summary>
    public class ModelValidatorContext
    {
        /// <summary>
        /// Model definition.
        /// </summary>
        public IModelDefinition Definition { get; private set; }

        /// <summary>
        /// Model values to validate.
        /// </summary>
        public IModelValueGetter Getter { get; private set; }

        /// <summary>
        /// Creates new instance. Parameters can't be <c>null</c>.
        /// </summary>
        /// <param name="definition">Model definition.</param>
        /// <param name="getter">Model values to validate.</param>
        public ModelValidatorContext(IModelDefinition definition, IModelValueGetter getter)
        {
            Ensure.NotNull(definition, "definition");
            Ensure.NotNull(getter, "getter");
            Definition = definition;
            Getter = getter;
        }
    }
}
