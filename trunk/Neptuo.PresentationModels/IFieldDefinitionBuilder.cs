using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Builds field definition.
    /// </summary>
    public interface IFieldDefinitionBuilder
    {
        /// <summary>
        /// Builds field definition.
        /// </summary>
        /// <returns>Field definition.</returns>
        IFieldDefinition Build();
    }
}
