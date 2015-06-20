using System;
using System.Collections.Generic;
using System.Linq;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Pohled pro jedno filtrační pole.
    /// </summary>
    /// <typeparam name="T">Typ kontextu, do kterého se má vykreslovat.</typeparam>
    public interface IFieldView<T>
    {
        /// <summary>
        /// Vykreslí pohled.
        /// </summary>
        /// <param name="target">Kontext, do kterého se má vykreslit.</param>
        void Render(T target);

        /// <summary>
        /// Vrací hodnotu zadanou do pohledu.
        /// </summary>
        /// <param name="value">Hodnotu zadanou do pohledu.</param>
        /// <returns>Zda bylo možné hodnotu získat.</returns>
        bool TryGetValue(out object value);

        /// <summary>
        /// Nastavuje hodnotu do pohledu.
        /// </summary>
        /// <param name="value">Nová hodnota.</param>
        /// <returns>Zda bylo možné hodnotu nastavit.</returns>
        bool TrySetValue(object value);
    }
}