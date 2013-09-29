using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Unity
{
    public class UnityDependencyContainer : IDependencyContainer
    {
        protected IUnityContainer UnityContainer { get; private set; }

        public UnityDependencyContainer()
            : this(new UnityContainer())
        { }

        public UnityDependencyContainer(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
        }

        public IDependencyContainer RegisterInstance(Type t, string name, object instance)
        {
            UnityContainer.RegisterType(t, name, new SingletonLifetimeManager(instance));
            return this;
        }

        public IDependencyContainer RegisterType(Type from, Type to, string name)
        {
            UnityContainer.RegisterType(from, to, name);
            return this;
        }

        public IDependencyContainer CreateChildContainer()
        {
            return new UnityDependencyContainer(UnityContainer.CreateChildContainer());
        }

        public object Resolve(Type t, string name)
        {
            return UnityContainer.Resolve(t, name);
        }

        public IEnumerable<object> ResolveAll(Type t)
        {
            return UnityContainer.ResolveAll(t);
        }
    }
}
