using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.Expressions
{
    public abstract class FuncPropertyValueProvider<TModel, TPropertyType> : IPropertyValueProvider<TModel>
    {
        public Func<TModel, TPropertyType> Getter { get; private set; }
        public Action<TModel, TPropertyType> Setter { get; private set; }

        public FuncPropertyValueProvider(Func<TModel, TPropertyType> getter, Action<TModel, TPropertyType> setter)
        {
            if (getter == null)
                throw new ArgumentNullException("getter");

            if (setter == null)
                throw new ArgumentNullException("setter");

            Getter = getter;
            Setter = setter;
        }

        public object GetValue(TModel model)
        {
            return Getter(model);
        }

        public void SetValue(TModel model, object value)
        {
            Setter(model, (TPropertyType)value);
        }
    }
}
