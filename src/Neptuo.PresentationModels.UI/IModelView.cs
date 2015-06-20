using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Definice serverového pohledu na model.
    /// </summary>
    public interface IModelView<T> : IModelValueProvider
    {
        /// <summary>
        /// Vykreslí model do <paramref name="parent"/>.
        /// </summary>
        /// <param name="parent">Kontext, do kterého se má model vykreslit.</param>
        void Render(T parent);
    }
}
