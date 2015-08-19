using Neptuo.Activators;
using Neptuo.Bootstrap.Handlers;
using Neptuo.Bootstrap.Internals;
using Neptuo.Bootstrap.Processing;
using Neptuo.Reflection;
using Neptuo.Reflection.Enumerators.Executors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing
{
    public class AutomaticLoaderBuilder : DisposableBase, ITypeDelegateCollection
    {
        private readonly IBootstrapHandlerExecutor handlerExecutor;
        private readonly Func<IEnumerable<Type>, IEnumerable<Type>> handlerCollectionDecorator;
        private readonly ITypeExecutorService typeExecutors;
        private readonly ITypeDelegateCollection filters;
        private readonly List<Type> handlerTypes = new List<Type>();
        private IFactory<IBootstrapHandler, Type> handlerFactory = new ActivatorFactory();

        public AutomaticLoaderBuilder(IBootstrapHandlerExecutor handlerExecutor)
            : this(handlerExecutor, types => types)
        { }

        public AutomaticLoaderBuilder(IBootstrapHandlerExecutor handlerExecutor, Func<IEnumerable<Type>, IEnumerable<Type>> handlerCollectionDecorator)
        {
            Ensure.NotNull(handlerExecutor, "handlerExecutor");
            Ensure.NotNull(handlerCollectionDecorator, "handlerCollectionDecorator");
            this.handlerExecutor = handlerExecutor;
            this.handlerCollectionDecorator = handlerCollectionDecorator;
            this.typeExecutors = ReflectionFactory.FromCurrentAppDomain().PrepareTypeExecutors();

            this.filters = typeExecutors
                .AddFiltered(false)
                .AddFilterNotAbstract()
                .AddFilterNotInterface()
                .AddFilterHasDefaultConstructor()
                .AddFilter(t => typeof(IBootstrapHandler).IsAssignableFrom(t))
                .AddHandler(handlerTypes.Add);
        }

        public ITypeDelegateCollection AddFilter(Func<Type, bool> filter)
        {
            filters.AddFilter(filter);
            return this;
        }

        public ITypeDelegateCollection AddHandler(Action<Type> handler)
        {
            filters.AddHandler(handler);
            return this;
        }

        public AutomaticLoaderBuilder AddHandlerFactory(IFactory<IBootstrapHandler, Type> handlerFactory)
        {
            Ensure.NotNull(handlerFactory, "handlerFactory");
            this.handlerFactory = handlerFactory;
            return this;
        }

        public IBootstrapper ToBootstrapper()
        {
            Ensure.NotDisposed(typeExecutors, "typeExecutors");
            typeExecutors.Dispose();

            IEnumerable<Type> handlerTypes = handlerCollectionDecorator(this.handlerTypes);
            return new DefaultBootstrapper(handlerExecutor, new HandlerEnumerator(handlerFactory, handlerTypes.GetEnumerator()));
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            typeExecutors.Dispose();
        }


        private class HandlerEnumerator : DisposableBase, IEnumerator<IBootstrapHandler>
        {
            private readonly IFactory<IBootstrapHandler, Type> handlerFactory;
            private readonly IEnumerator<Type> handlerTypeEnumerator;

            public IBootstrapHandler Current { get; private set; }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public HandlerEnumerator(IFactory<IBootstrapHandler, Type> handlerFactory, IEnumerator<Type> handlerTypeEnumerator)
            {
                this.handlerFactory = handlerFactory;
                this.handlerTypeEnumerator = handlerTypeEnumerator;
            }

            public bool MoveNext()
            {
                if (handlerTypeEnumerator.MoveNext())
                {
                    Current = handlerFactory.Create(handlerTypeEnumerator.Current);
                    return true;
                }

                Current = null;
                return false;
            }

            public void Reset()
            {
                handlerTypeEnumerator.Reset();
            }
        }

    }
}
