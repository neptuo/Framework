using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Kolekce poskytující registrované pohledy pro fieldy.
    /// </summary>
    /// <typeparam name="T">Typ kontextu pohledu.</typeparam>
    public interface IFieldViewCollection<T>
    {
        /// <summary>
        /// Pokusí se vrátit pohled pro model identifikovaný <paramref name="modelIdentifier"/>.
        /// </summary>
        /// <param name="fieldView">Zaregistrvaný pohled.</param>
        /// <returns><c>true</c> pokud se povedlo pohled najít; jinak <c>false</c>.</returns>
        bool TryGet(IModelDefinition modelDefinition, IFieldDefinition fieldDefinition, out IFieldView<T> fieldView);
    }
}
