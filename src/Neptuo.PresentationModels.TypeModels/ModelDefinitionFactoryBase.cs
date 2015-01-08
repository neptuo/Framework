using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    public abstract class ModelDefinitionFactoryBase : IModelDefinitionFactory
    {
        protected Dictionary<Type, IModelDefinition> Storage { get; private set; }

        public ModelDefinitionFactoryBase()
        {
            Storage = new Dictionary<Type, IModelDefinition>();
        }

        public IModelDefinition Create(Type modelType)
        {
            if (modelType == null)
                throw new ArgumentNullException("modelType");

            IModelDefinition modelDefinition;
            if (!Storage.TryGetValue(modelType, out modelDefinition))
            {
                IModelDefinitionBuilder builder = CreateModelDefinitionBuilder(modelType);
                modelDefinition = builder.Build();
                Storage[modelType] = modelDefinition;
            }
            return modelDefinition;
        }

        protected abstract IModelDefinitionBuilder CreateModelDefinitionBuilder(Type modelType);
    }
}
