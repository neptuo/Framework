using System;
using System.Collections.Generic;
using System.Linq;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Single field view.
    /// </summary>
    /// <typeparam name="T">Type of rendering context.</typeparam>
    public interface IFieldView<T>
    {
        /// <summary>
        /// Renders the view to the <paramref name="target"/>.
        /// </summary>
        /// <param name="target">Rendering context.</param>
        void Render(T target);

        /// <summary>
        /// Tries to get current value of the view.
        /// </summary>
        /// <param name="value">Current view value.</param>
        /// <returns><c>true</c> if value was provided; <c>false</c> otherwise.</returns>
        bool TryGetValue(out object value);

        /// <summary>
        /// Tries to set new value to the view.
        /// </summary>
        /// <param name="value">New view value.</param>
        /// <returns><c>true</c> if value was set; <c>false</c> otherwise.</returns>
        bool TrySetValue(object value);
    }
}