using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// A container for <see cref="IModelViewProvider{T}"/>.
    /// </summary>
    /// <typeparam name="T">A type of the rendering context.</typeparam>
    public interface IModelViewProviderContainer<T>
    {
        /// <summary>
        /// Gets a provider of model views.
        /// </summary>
        IModelViewProvider<T> ModelViewProvider { get; }
    }
}
