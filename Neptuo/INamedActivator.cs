using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// Activator for <typeparamref name="T"/> for named registraions.
    /// </summary>
    /// <typeparam name="T">Type of service to create.</typeparam>
    public interface INamedActivator<T>
    {
        /// <summary>
        /// Creates named service of type <typeparamref name="T"/> and name <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Name (like discriminator).</param>
        /// <returns>Named service of type <typeparamref name="T"/>.</returns>
        T Create(string name);
    }
}
