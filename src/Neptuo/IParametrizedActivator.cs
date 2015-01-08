using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// Activator for <typeparamref name="T"/> with posibility to use parameters for inicialization.
    /// </summary>
    /// <typeparam name="T">Type of service to create.</typeparam>
    public interface IParametrizedActivator<T>
    {
        /// <summary>
        /// Creates service of type <typeparamref name="T"/> with posibility to use <paramref name="parameters"/> for inicialization.
        /// </summary>
        /// <param name="parameters">Collection of parameters that can be used for inicialization.</param>
        /// <returns>Service of type <typeparamref name="T"/>.</returns>
        T Create(IReadOnlyKeyValueCollection parameters);
    }
}
