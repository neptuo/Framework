using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.Expressions
{
    /// <summary>
    /// Delegate based implementation of <see cref="IFieldValueProvider{TModel}"/>.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    /// <typeparam name="TPropertyType">Type of field.</typeparam>
    public abstract class FuncFieldValueProvider<TModel, TPropertyType> : IFieldValueProvider<TModel>
    {
        public Func<TModel, TPropertyType> Getter { get; private set; }
        public Action<TModel, TPropertyType> Setter { get; private set; }

        public FuncFieldValueProvider(Func<TModel, TPropertyType> getter, Action<TModel, TPropertyType> setter)
        {
            Ensure.NotNull(getter, "getter");
            Ensure.NotNull(setter, "setter");
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
