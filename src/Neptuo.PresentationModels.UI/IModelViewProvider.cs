using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Collection of model views.
    /// </summary>
    /// <typeparam name="T">Type of rendering context.</typeparam>
    public interface IModelViewProvider<T>
    {
        /// <summary>
        /// Tries to get model view for model definition.
        /// </summary>
        /// <param name="modelView">Instance of registered model view..</param>
        /// <returns><c>true</c> if field view was found; <c>false</c> otherwise.</returns>
        bool TryGet(IModelDefinition modelDefinition, out IModelView<T> modelView);
    }
}
