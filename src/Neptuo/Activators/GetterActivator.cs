using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Implementation of <see cref="IActivator{T}"/> that for every call to create instance calls passed delegate.
    /// </summary>
    public class GetterActivator<T> : IActivator<T>
    {
        private readonly Func<T> getter;

        /// <summary>
        /// Creates new instance that uses <paramref name="getter"/> to providing instances.
        /// </summary>
        /// <param name="getter">Instances provider delegate.</param>
        public GetterActivator(Func<T> getter)
        {
            Ensure.NotNull(getter, "getter");
            this.getter = getter;
        }

        public T Create()
        {
            return getter();
        }
    }

    /// <summary>
    /// Implementation of <see cref="IActivator{T, TContext}"/> that for every call to create instance calls passed delegate.
    /// </summary>
    public class GetterActivator<T, TContext> : IActivator<T, TContext>
    {
        private readonly Func<TContext, T> getter;

        /// <summary>
        /// Creates new instance that uses <paramref name="getter"/> to providing instances.
        /// </summary>
        /// <param name="getter">Instances provider delegate.</param>
        public GetterActivator(Func<TContext, T> getter)
        {
            Ensure.NotNull(getter, "getter");
            this.getter = getter;
        }

        public T Create(TContext context)
        {
            return getter(context);
        }
    }
}
