using Neptuo.Models;
using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Deleters.Handlers
{
    /// <summary>
    /// Base implementation of <see cref="IDeleteHandler"/> when model instance is required for preparing <see cref="IDeleteContext"/>.
    /// When there isn't model associated with key, empty context is returned.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    /// <typeparam name="TKey">Type of model's key.</typeparam>
    public abstract class ModelDeleteHandlerBase<TModel, TKey> : IDeleteHandler
        where TModel : IModel<TKey>
        where TKey : class, IKey
    {
        private readonly IReadOnlyRepository<TModel, TKey> repository;

        /// <summary>
        /// Creates new instance that reads model of <paramref name="repository"/>.
        /// </summary>
        /// <param name="repository">Repository to read model from.</param>
        protected ModelDeleteHandlerBase(IReadOnlyRepository<TModel, TKey> repository)
        {
            Ensure.NotNull(repository, "repository");
            this.repository = repository;
        }

        public IDeleteContext Handle(IKey key)
        {
            TKey modelKey = key as TKey;
            Ensure.NotNull(modelKey, "key");

            TModel model = repository.Find(modelKey);
            if (model == null)
                return new MissingHandlerContext(key);

            return HandleModel(model);
        }

        /// <summary>
        /// Prepares <see cref="IDeleteContext"/> for <paramref name="model"/>.
        /// </summary>
        /// <param name="key">Model to delete.</param>
        /// <returns>Deletion context.</returns>
        protected abstract IDeleteContext HandleModel(TModel model);
    }
}
