using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// Factory for reading and writing XML files and creating model value getters and setters from it.
    /// </summary>
    public class XmlModelValueProviderFactory
    {
        private readonly IModelDefinition modelDefinition;

        public XmlModelValueProviderFactory(IModelDefinition modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            this.modelDefinition = modelDefinition;
        }


    }
}
