using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.Expressions
{
    public class DictionaryPropertyValueProviderCollection<TModel> : Dictionary<string, IPropertyValueProvider<TModel>>, IPropertyValueProviderCollection<TModel>
    { }
}
