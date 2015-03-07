using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Implementation of <see cref="IActivator{T}"/> by <see cref="IUnityContainer"/>.
    /// </summary>
    /// <typeparam name="T">Type of service to create.</typeparam>
    public class UnityActivator<T> : IActivator<T>
    {
        private readonly IUnityContainer unityContainer;

        public UnityActivator(IUnityContainer unityContainer)
        {
            Guard.NotNull(unityContainer, "unityContainer");
            this.unityContainer = unityContainer;
        }

        public T Create()
        {
            return unityContainer.Resolve<T>();
        }
    }
}
