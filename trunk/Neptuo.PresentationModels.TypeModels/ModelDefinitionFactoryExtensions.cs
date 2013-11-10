using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    public static class ModelDefinitionFactoryExtensions
    {
        public static IModelDefinition Create<TModel>(this IModelDefinitionFactory factory)
        {
            return factory.Create(typeof(TModel));
        }
    }
}
