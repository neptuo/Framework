using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.Expressions
{
    public class ExpressionModelValueProvider<TModel> : IModelValueProvider
        where TModel : class
    {
        public TModel Model { get; private set; }
        protected IPropertyValueProviderCollection<TModel> ValueProviders { get; private set; }

        public ExpressionModelValueProvider(TModel model, IPropertyValueProviderCollection<TModel> valueProviders)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            Model = model;

            if (valueProviders != null)
                ValueProviders = valueProviders;
            else
                ValueProviders = new DictionaryPropertyValueProviderCollection<TModel>();
        }

        public ExpressionModelValueProvider<TModel> Add(string identifier, IPropertyValueProvider<TModel> valueProvider)
        {
            ValueProviders[identifier] = valueProvider;
            return this;
        }

        public bool TryGetValue(string identifier, out object value)
        {
            value = GetValueProvider(identifier).GetValue(Model);
            return true;
        }

        public void SetValue(string identifier, object value)
        {
            GetValueProvider(identifier).SetValue(Model, value);
        }

        protected IPropertyValueProvider<TModel> GetValueProvider(string identifier)
        {
            IPropertyValueProvider<TModel> valueProvider;
            if (!ValueProviders.TryGetValue(identifier, out valueProvider))
                throw new ArgumentOutOfRangeException("identifier", String.Format("Unnable to find property '{0}'", identifier));

            return valueProvider;
        }
    }
}
