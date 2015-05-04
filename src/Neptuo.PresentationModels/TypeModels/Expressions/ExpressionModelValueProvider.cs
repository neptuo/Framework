using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.Expressions
{
    public class ExpressionModelValueProvider<TModel> : DisposableBase, IModelValueProvider
        where TModel : class
    {
        public TModel Model { get; private set; }
        protected IFieldValueProviderCollection<TModel> ValueProviders { get; private set; }

        public ExpressionModelValueProvider(TModel model, IFieldValueProviderCollection<TModel> valueProviders)
        {
            Ensure.NotNull(model, "model");
            Ensure.NotNull(valueProviders, "valueProviders");
            Model = model;
            ValueProviders = valueProviders;
        }

        public bool TryGetValue(string identifier, out object value)
        {
            value = GetValueProvider(identifier).GetValue(Model);
            return true;
        }

        public bool TrySetValue(string identifier, object value)
        {
            GetValueProvider(identifier).SetValue(Model, value);
            return true;
        }

        protected IFieldValueProvider<TModel> GetValueProvider(string identifier)
        {
            IFieldValueProvider<TModel> valueProvider;
            if (!ValueProviders.TryGet(identifier, out valueProvider))
                throw Ensure.Exception.ArgumentOutOfRange("identifier", "Unnable to find property '{0}'.", identifier);

            return valueProvider;
        }
    }
}
