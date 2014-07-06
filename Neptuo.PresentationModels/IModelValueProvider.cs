using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Current value getter and setter.
    /// Provides methods for reading and writing values.
    /// </summary>
    public interface IModelValueProvider : IModelValueGetter, IModelValueSetter
    { }
}
