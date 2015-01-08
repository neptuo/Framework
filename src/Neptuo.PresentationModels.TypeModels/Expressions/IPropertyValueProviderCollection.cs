using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.Expressions
{
    public interface IPropertyValueProviderCollection<TModel>
    {
        IPropertyValueProvider<TModel> this[string identifier] { set; }
        bool TryGetValue(string idenfitifer, out IPropertyValueProvider<TModel> value);
    }
}
