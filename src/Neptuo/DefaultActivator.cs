using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// Implementation of <see cref="IActivator{T}"/> for types with parameterless constructor.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultActivator<T> : IActivator<T>
        where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }
}
