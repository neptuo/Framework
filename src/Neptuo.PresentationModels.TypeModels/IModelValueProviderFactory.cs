using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    public interface IModelValueProviderFactory
    {
        IModelValueProvider Create(object instance);
    }
}
