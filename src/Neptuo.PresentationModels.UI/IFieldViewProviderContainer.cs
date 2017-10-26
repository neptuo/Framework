using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// A container for <see cref="IFieldViewProvider{T}"/>.
    /// </summary>
    /// <typeparam name="T">A type of the rendering context.</typeparam>
    public interface IFieldViewProviderContainer<T>
    {
        /// <summary>
        /// Gets a provider of field views.
        /// </summary>
        IFieldViewProvider<T> FieldViewProvider { get; }
    }
}
