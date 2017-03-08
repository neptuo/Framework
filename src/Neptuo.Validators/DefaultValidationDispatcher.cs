using Neptuo;
using Neptuo.Validators.Handlers;
using Neptuo.Validators.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    /// <summary>
    /// Default implementation of <see cref="IValidationDispatcher"/> and <see cref="IValidationHandlerCollection"/>.
    /// When handling model without registered handler, exception is thrown.
    /// </summary>
    public class DefaultValidationDispatcher : ValidationDispatcherBase, IValidationHandlerCollection
    {
        private readonly Dictionary<Type, DefaultValidationHandlerDefinition> storage = new Dictionary<Type, DefaultValidationHandlerDefinition>();
        private readonly OutFuncCollection<Type, object, bool> onSearchHandler = new OutFuncCollection<Type, object, bool>();

        /// <summary>
        /// Creates a new instance and an exception is thrown for missing handler.
        /// </summary>
        public DefaultValidationDispatcher()
        { }

        /// <summary>
        /// Creates a new instance and a valid or invalid result is returned for missing handler.
        /// </summary>
        /// <param name="isMissingHandlerValid">Whether a missing handler should return valid result or invalid.</param>
        public DefaultValidationDispatcher(bool isMissingHandlerValid) 
            : base(isMissingHandlerValid)
        { }

        public IValidationHandlerCollection Add<TModel>(IValidationHandler<TModel> handler)
        {
            Ensure.NotNull(handler, "handler");
            Type modelType = typeof(TModel);
            storage[modelType] = new DefaultValidationHandlerDefinition(handler, model => handler.HandleAsync((TModel)model));
            return this;
        }

        public IValidationHandlerCollection AddSearchHandler(OutFunc<Type, object, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");
            onSearchHandler.Add(searchHandler);
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

            object rawHandler;
            if (onSearchHandler.TryExecute(modelType, out rawHandler))
            {
                handler = definition.ValidationHandler as IValidationHandler<TModel>;
                return handler != null;
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

            if (onSearchHandler.TryExecute(modelType, out validationHandler))
                return true;

            validationHandler = null;
            return false;
        }
    }
}
