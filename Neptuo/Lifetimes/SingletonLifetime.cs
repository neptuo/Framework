using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Lifetimes
{
    public class SingletonLifetime<T>
    {
        public T Instance { get; private set; }
        public Action<T> Initialize { get; private set; }

        public SingletonLifetime()
        { }

        public SingletonLifetime(T instance)
        {
            Guard.NotNull(instance, "instance");
            Instance = instance;
        }

        public SingletonLifetime(Action<T> initialize)
        {
            Guard.NotNull(initialize, "initialize");
            this.Initialize = initialize;
        }

        public SingletonLifetime(T instance, Action<T> initialize)
            : this(instance)
        {
            Guard.NotNull(instance, "instance");
            Guard.NotNull(initialize, "initialize");
            this.Initialize = initialize;
        }

    }

    public class SingletonLifetime : SingletonLifetime<object>
    {
        public SingletonLifetime()
        { }

        public SingletonLifetime(object instance)
            : base(instance)
        { }

        public SingletonLifetime(Action<object> initialize)
            : base(initialize)
        { }

        public SingletonLifetime(object instance, Action<object> initialize)
            : base(instance, initialize)
        { }
    }
}
