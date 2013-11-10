using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    public class ReflectionModelDefinitionFactory : ModelDefinitionFactoryBase
    {
        protected MetadataReaderService MetadataReaders { get; private set; }

        public ReflectionModelDefinitionFactory(MetadataReaderService metadataReaders)
        {
            if (metadataReaders == null)
                throw new ArgumentNullException("metadataReaders");

            MetadataReaders = metadataReaders;
        }

        protected override IModelDefinitionBuilder CreateModelDefinitionBuilder(Type modelType)
        {
            return new ReflectionModelDefinitionBuilder(modelType, MetadataReaders);
        }
    }
}
