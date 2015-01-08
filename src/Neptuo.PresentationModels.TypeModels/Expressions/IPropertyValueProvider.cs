using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.Expressions
{
    public interface IPropertyValueProvider<TModel>
    {
        object GetValue(TModel model);
        void SetValue(TModel model, object value);
    }
}
