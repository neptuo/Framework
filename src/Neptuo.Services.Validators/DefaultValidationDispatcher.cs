using Neptuo.Services.Validators.Handlers;
using Neptuo.Services.Validators.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Validators
{
    /// <summary>
    /// Default implementation of <see cref="IValidationDispatcher"/> and <see cref="IValidationHandlerCollection"/>.
    /// When handling model without registered handler, exception is thrown.
    /// </summary>
    public class DefaultValidationDispatcher : ValidationDispatcherBase, IValidationHandlerCollection
    {
        private readonly Dictionary<Type, DefaultValidationHandlerDefinition> storage = new Dictionary<Type, DefaultValidationHandlerDefinition>();

        public IValidationHandlerCollection Add<TModel>(IValidationHandler<TModel> handler)
        {
            Ensure.NotNull(handler, "handler");
            Type modelType = typeof(TModel);
            storage[modelType] = new DefaultValidationHandlerDefinition(handler, model => handler.HandleAsync((TModel)model));
            return this;
        }

        public bool TryGet<TModel>(out IValidationHandler<TModel> handler)
        {
            Type modelType = typeof(TModel);
            DefaultValidationHandlerDefinition definition;
            if (storage.TryGetValue(modelType, out definition))
            {
                handler = (IValidationHandler<TModel>)definition.ValidationHandler;
                return true;
            }

            handler = null;
            return false;
        }

        protected override bool TryGetValidationHandler(Type modelType, out object validationHandler)
        {
            DefaultValidationHandlerDefinition definition;
            if (storage.TryGetValue(modelType, out definition))
            {
                validationHandler = definition.ValidationHandler;
                return true;
            }

            validationHandler = null;
            return false;
        }
    }
}
