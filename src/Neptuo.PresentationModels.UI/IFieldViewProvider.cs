using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Collection of field views.
    /// </summary>
    /// <typeparam name="T">Type of rendering context.</typeparam>
    public interface IFieldViewProvider<T>
    {
        /// <summary>
        /// Tries to get field view for field and model definition.
        /// </summary>
        /// <param name="modelDefinition">Model definition that contains <paramref name="fieldDefinition" /></param>
        /// <param name="fieldDefinition">Field definition to find field view for.</param>
        /// <param name="fieldView">Instance of registered field view.</param>
        /// <returns><c>true</c> if field view was found; <c>false</c> otherwise.</returns>
        bool TryGet(IModelDefinition modelDefinition, IFieldDefinition fieldDefinition, out IFieldView<T> fieldView);
    }
}
