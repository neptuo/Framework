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
        private readonly string name;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="unityContainer">Unity container to use for instance creation.</param>
        /// <param name="name">Optional registration name.</param>
        public UnityActivator(IUnityContainer unityContainer, string name = null)
        {
            Guard.NotNull(unityContainer, "unityContainer");
            this.unityContainer = unityContainer;
            this.name = name;
        }

        public T Create()
        {
            return unityContainer.Resolve<T>(name);
        }
    }
}
