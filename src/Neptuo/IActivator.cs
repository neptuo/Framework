using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// Activator for <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">Type of service to create.</typeparam>
    public interface IActivator<T>
    {
        /// <summary>
        /// Creates service of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Service of type <typeparamref name="T"/>.</returns>
        T Create();
    }
}
