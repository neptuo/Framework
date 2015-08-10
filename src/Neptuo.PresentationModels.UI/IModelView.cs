using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Complete model view.
    /// </summary>
    /// <typeparam name="T">Type of rendering context.</typeparam>
    public interface IModelView<T> : IModelValueProvider
    {
        /// <summary>
        /// Renders the view to the <paramref name="target"/>.
        /// </summary>
        /// <param name="target">Rendering context.</param>
        void Render(T parent);
    }
}