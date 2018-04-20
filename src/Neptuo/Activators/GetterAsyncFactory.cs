using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// An implementation of the <see cref="IFactory{T}"/> that for every call to create instance calls passed delegate.
    /// </summary>
    public class GetterAsyncFactory<T> : IAsyncFactory<T>
    {
        private readonly Func<Task<T>> getter;

        /// <summary>
        /// Creates new instance that uses <paramref name="getter"/> for providing instances.
        /// </summary>
        /// <param name="getter">An instance provider delegate.</param>
        public GetterAsyncFactory(Func<T> getter)
        {
            Ensure.NotNull(getter, "getter");
            this.getter = () => Task.FromResult(getter());
        }

        /// <summary>
        /// Creates new instance that uses <paramref name="getter"/> for providing instances.
        /// </summary>
        /// <param name="getter">An instance provider delegate.</param>
        public GetterAsyncFactory(Func<Task<T>> getter)
        {
            Ensure.NotNull(getter, "getter");
            this.getter = getter;
        }

        public Task<T> Create()
            => getter();
    }

    /// <summary>
    /// An implementation of the <see cref="IFactory{T, TContext}"/> that for every call to create instance calls passed delegate.
    /// </summary>
    public class GetterAsyncFactory<T, TContext> : IAsyncFactory<T, TContext>
    {
        private readonly Func<TContext, Task<T>> getter;

        /// <summary>
        /// Creates a new instance that uses <paramref name="getter"/> for providing instances.
        /// </summary>
        /// <param name="getter">An instance provider delegate.</param>
        public GetterAsyncFactory(Func<TContext, T> getter)
        {
            Ensure.NotNull(getter, "getter");
            this.getter = context => Task.FromResult(getter(context));
        }

        /// <summary>
        /// Creates a new instance that uses <paramref name="getter"/> for providing instances.
        /// </summary>
        /// <param name="getter">An instance provider delegate.</param>
        public GetterAsyncFactory(Func<TContext, Task<T>> getter)
        {
            Ensure.NotNull(getter, "getter");
            this.getter = getter;
        }

        public Task<T> Create(TContext context)
            => getter(context);
    }
}
