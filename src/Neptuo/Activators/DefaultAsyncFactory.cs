using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// An implementation of the <see cref="IFactory{T}"/> for types with parameterless constructor.
    /// </summary>
    public class DefaultAsyncFactory<T> : IAsyncFactory<T>
        where T : new()
    {
        public Task<T> Create()
            => Task.FromResult(new T());
    }
}
