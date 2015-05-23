using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Common extensions for <see cref="IModelDefinition"/>.
    /// </summary>
    public static class _ModelDefinitionExtensions
    {
        /// <summary>
        /// Returns fields from <paramref name="modelDefinition"/> mapped to dictionary by <see cref="IFieldDefinition.Identifier"/>.
        /// </summary>
        /// <param name="modelDefinition">Source model definition to read fields from.</param>
        /// <returns>Fields from <paramref name="modelDefinition"/> mapped to dictionary by <see cref="IFieldDefinition.Identifier"/>.</returns>
        public static Dictionary<string, IFieldDefinition> FieldsByIdentifier(this IModelDefinition modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Dictionary<string, IFieldDefinition> result = new Dictionary<string, IFieldDefinition>();
            foreach (IFieldDefinition fieldDefinition in modelDefinition.Fields)
                result[fieldDefinition.Identifier] = fieldDefinition;

            return result;
        }
    }
}
