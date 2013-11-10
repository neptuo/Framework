using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    public interface IModelDefinitionFactory
    {
        IModelDefinition Create(Type modelType);
    }
}
