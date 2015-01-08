using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Current value setter.
    /// <summary>
    public interface IModelValueSetter
    {
        /// <summary>
        /// Sets value of field with <paramref name="identifier"/> to <paramref name="value"/>.
        /// </summary>
        /// <param name="identifier">Field identifier to set value.</param>
        /// <param name="value">New value for field with <paramref name="identifier"/>.</param>
        void SetValue(string identifier, object value);
    }
}
