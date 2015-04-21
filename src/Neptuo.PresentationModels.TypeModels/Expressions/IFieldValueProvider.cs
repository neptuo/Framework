using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.Expressions
{
    /// <summary>
    /// Single field value provider.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    public interface IFieldValueProvider<TModel>
    {
        /// <summary>
        /// Gets value from the field.
        /// </summary>
        /// <param name="model">Instance of model.</param>
        /// <returns>Value of field in <paramref name="model"/>.</returns>
        object GetValue(TModel model);

        /// <summary>
        /// Sets value to the field.
        /// </summary>
        /// <param name="model">Instance of model.</param>
        /// <param name="value">New value of model.</param>
        void SetValue(TModel model, object value);
    }
}
