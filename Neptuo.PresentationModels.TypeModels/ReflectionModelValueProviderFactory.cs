using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    public class ReflectionModelValueProviderFactory : IModelValueProviderFactory
    {
        public IModelValueProvider Create(object instance)
        {
            return new ReflectionModelValueProvider(instance);
        }
    }
}
