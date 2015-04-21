using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Base implementation for building <see cref="IFieldDefinitionBuilder"/>.
    /// </summary>
    public abstract class FieldDefinitionBuilderBase : IFieldDefinitionBuilder
    {
        /// <summary>
        /// Provides field identifier.
        /// </summary>
        /// <returns>Field identifier.</returns>
        protected abstract string BuildFieldIdentifier();

        /// <summary>
        /// Provides field type.
        /// </summary>
        /// <returns>Field type.</returns>
        protected abstract Type BuildFieldType();

        /// <summary>
        /// Provides field metadata.
        /// </summary>
        /// <returns>Field metadata.</returns>
        protected abstract IKeyValueCollection BuildFieldMetadata();

        /// <summary>
        /// Builds field definition using <see cref="FieldDefinition"/>.
        /// </summary>
        /// <returns>Field definition.</returns>
        public IFieldDefinition Build()
        {
            return new FieldDefinition(BuildFieldIdentifier(), BuildFieldType(), BuildFieldMetadata());
        }
    }
}
