using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Builds model definition.
    /// </summary>
    public interface IModelDefinitionBuilder
    {
        /// <summary>
        /// Builds model definition.
        /// </summary>
        /// <returns>Model definition.</returns>
        IModelDefinition Build();
    }
}
