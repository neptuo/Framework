using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Definition of whole model.
    /// </summary>
    public interface IModelDefinition
    {
        /// <summary>
        /// Model identifier.
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// Enumeration of fields.
        /// </summary>
        IEnumerable<IFieldDefinition> Fields { get; }

        /// <summary>
        /// Model metadata collection.
        /// </summary>
        IModelMetadataCollection Metadata { get; }
    }
}
