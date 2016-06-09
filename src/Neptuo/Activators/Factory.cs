using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Factory class for the instance of <see cref="IFactory{T}"/>.
    /// </summary>
    public static class Factory
    {
        public static IFactory<T> Getter<T>(Func<T> getter)
        {
            return new GetterFactory<T>(getter);
        }

        public static IFactory<T> Instance<T>(T instance)
        {
            return new InstanceFactory<T>(instance);
        }

        public static IFactory<T> Instance<T>(Func<T> getter)
        {
            return new InstanceFactory<T>(getter);
        }

        public static IFactory<T> Default<T>()
            where T : new()
        {
            return new DefaultFactory<T>();
        }
    }
}
