using Neptuo.Activators;
using Neptuo.Bootstrap.Handlers;
using Neptuo.Bootstrap.Processing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing
{
    public class ManualLoaderBuilder : IBootstrapHandlerCollection
    {
        private readonly Dictionary<Type, IFactory<IBootstrapHandler>> storage = new Dictionary<Type, IFactory<IBootstrapHandler>>();
        private readonly Func<IEnumerable<Type>, IEnumerable<Type>> handlerCollectionDecorator;
        private readonly IBootstrapHandlerExecutor handlerExecutor;

        public ManualLoaderBuilder(IBootstrapHandlerExecutor handlerExecutor)
            : this(handlerExecutor, types => types)
        { }

        public ManualLoaderBuilder(IBootstrapHandlerExecutor handlerExecutor, Func<IEnumerable<Type>, IEnumerable<Type>> handlerCollectionDecorator)
        {
            Ensure.NotNull(handlerExecutor, "handlerExecutor");
            this.handlerExecutor = handlerExecutor;
        }

        public IBootstrapHandlerCollection Add<T>(IFactory<T> factory)
            where T : class, IBootstrapHandler
        {
            Ensure.NotNull(factory, "factory");
            storage[typeof(T)] = factory;
            return this;
        }

        public bool TryGet<T>(out IFactory<T> factory)
            where T : class, IBootstrapHandler
        {
            IFactory<IBootstrapHandler> innerFactory;
            if (storage.TryGetValue(typeof(T), out innerFactory))
            {
                factory = (IFactory<T>)innerFactory;
                return true;
            }

            factory = null;
            return true;
        }

        public IBootstrapper ToBootstrapper()
        {
            IEnumerable<Type> handlerTypes = handlerCollectionDecorator(storage.Keys);
            return new DefaultBootstrapper(handlerExecutor, new HandlerEnumerator(handlerTypes.GetEnumerator(), type => storage[type].Create()));
        }


        private class HandlerEnumerator : DisposableBase, IEnumerator<IBootstrapHandler>
        {
            private readonly IEnumerator<Type> handlerTypes;
            private readonly Func<Type, IBootstrapHandler> handlerGetter;

            public IBootstrapHandler Current { get; private set; }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public HandlerEnumerator(IEnumerator<Type> handlerTypes, Func<Type, IBootstrapHandler> handlerGetter)
            {
                this.handlerTypes = handlerTypes;
                this.handlerGetter = handlerGetter;
            }

            public bool MoveNext()
            {
                if (handlerTypes.MoveNext())
                {
                    Current = handlerGetter(handlerTypes.Current);
                    return true;
                }

                Current = null;
                return false;
            }

            public void Reset()
            {
                handlerTypes.Reset();
            }
        }
    }
}
