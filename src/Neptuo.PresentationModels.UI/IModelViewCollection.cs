using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Kolekce poskytující registrované pohledy pro celé modely.
    /// </summary>
    /// <typeparam name="T">Typ kontextu pohledu.</typeparam>
    public interface IModelViewCollection<T>
    {
        /// <summary>
        /// Pokusí se vrátit pohled pro model identifikovaný <paramref name="modelDefinition"/>.
        /// </summary>
        /// <param name="modelDefinition">Definice modelu.</param>
        /// <param name="modelView">Zaregistrvaný pohled.</param>
        /// <returns><c>true</c> pokud se povedlo pohled najít; jinak <c>false</c>.</returns>
        bool TryGet(IModelDefinition modelDefinition, out IModelView<T> modelView);
    }
}
