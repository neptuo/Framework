using Neptuo;
using Neptuo.Activators;
using Neptuo.ComponentModel;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Kolekce aktivátorů <see cref="IModelView"/>.
    /// </summary>
    public class ModelViewCollection<T> : IModelViewCollection<T>
    {
        private readonly Dictionary<string, IActivator<IModelView<T>>> storage = new Dictionary<string, IActivator<IModelView<T>>>();
        private readonly OutFuncCollection<IModelDefinition, IActivator<IModelView<T>>, bool> onSearchView = new OutFuncCollection<IModelDefinition, IActivator<IModelView<T>>, bool>();

        /// <summary>
        /// Spáruje <paramref name="modelIdentifier"/> s pohledem <paramref name="modelViewActivator"/>.
        /// </summary>
        /// <param name="modelIdentifier">Identifikátor definice modelu.</param>
        /// <param name="modelViewActivator">Pohled, který se pro daný model má použít.</param>
        /// <returns>Sebe (kvůli fluentnosti).</returns>
        public ModelViewCollection<T> Add(string modelIdentifier, IActivator<IModelView<T>> modelViewActivator)
        {
            Ensure.NotNullOrEmpty(modelIdentifier, "modelIdentifier");
            Ensure.NotNull(modelViewActivator, "modelViewActivator");
            storage[modelIdentifier] = modelViewActivator;
            return this;
        }

        /// <summary>
        /// Spáruje delegáta <paramref name="searchHandler" />, který bude spuštěn, 
        /// pokud se nepovedlo najít zaregistrovaný pohled pro model.
        /// </summary>
        /// <param name="searchHandler">Delegát, který může vrátit pohled.</param>
        /// <returns>Sebe (kvůli fluentnosti).</returns>
        public ModelViewCollection<T> AddSearchHandler(OutFunc<IModelDefinition, IActivator<IModelView<T>>, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");
            onSearchView.Add(searchHandler);
            return this;
        }

        /// <summary>
        /// Pokusí se vrátit pohled pro model identifikovaný <paramref name="modelDefinition"/>.
        /// </summary>
        /// <param name="modelDefinition">Definice modelu.</param>
        /// <param name="modelView">Zaregistrvaný pohled.</param>
        /// <returns><c>true</c> pokud se povedlo pohled najít; jinak <c>false</c>.</returns>
        public bool TryGet(IModelDefinition modelDefinition, out IModelView<T> modelView)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            IActivator<IModelView<T>> modelViewActivator;
            if (storage.TryGetValue(modelDefinition.Identifier, out modelViewActivator))
            {
                modelView = modelViewActivator.Create();
                return true;
            }

            if (onSearchView.TryExecute(modelDefinition, out modelViewActivator))
            {
                modelView = modelViewActivator.Create();
                return true;
            }

            modelView = null;
            return false;
        }
    }
}
