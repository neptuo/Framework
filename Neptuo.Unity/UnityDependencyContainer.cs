using Microsoft.Practices.Unity;
using Neptuo.Lifetimes;
using Neptuo.Lifetimes.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Unity
{
    public class UnityDependencyContainer : IDependencyContainer, ILifetimeMapping<LifetimeManager>
    {
        protected bool IsChildMappingCreated { get; set; }
        protected IUnityContainer UnityContainer { get; private set; }
        protected LifetimeMapping<LifetimeManager> LifetimeMapping { get; private set; }

        public UnityDependencyContainer()
            : this(new UnityContainer(), new LifetimeMapping<LifetimeManager>())
        {
            IsChildMappingCreated = true;
        }

        public UnityDependencyContainer(IUnityContainer unityContainer, LifetimeMapping<LifetimeManager> lifetimeMapping)
        {
            if (unityContainer == null)
                throw new ArgumentNullException("unityContainer");

            if (lifetimeMapping == null)
                throw new ArgumentNullException("lifetimeMapping");

            UnityContainer = unityContainer;
            LifetimeMapping = lifetimeMapping;
        }

        public IDependencyContainer RegisterInstance(Type t, string name, object instance)
        {
            UnityContainer.RegisterType(t, name, new SingletonLifetimeManager(instance));
            return this;
        }

        public IDependencyContainer RegisterType(Type from, Type to, string name, object lifetime)
        {
            LifetimeManager lifetimeManager = null;
            if(lifetime != null)
                lifetimeManager = LifetimeMapping.Resolve(lifetime);

            UnityContainer.RegisterType(from, to, name, lifetimeManager, new InjectionMember[0]);
            return this;
        }

        public IDependencyContainer CreateChildContainer()
        {
            return new UnityDependencyContainer(UnityContainer.CreateChildContainer(), LifetimeMapping);
        }

        public object Resolve(Type t, string name)
        {
            return UnityContainer.Resolve(t, name);
        }

        public IEnumerable<object> ResolveAll(Type t)
        {
            return UnityContainer.ResolveAll(t);
        }


        public UnityDependencyContainer Map(Type lifetimeType, ILifetimeMapper<LifetimeManager> mapper)
        {
            if (!IsChildMappingCreated)
            {
                LifetimeMapping = LifetimeMapping.CreateChildMapping();
                IsChildMappingCreated = true;
            }

            LifetimeMapping.Map(lifetimeType, mapper);
            return this;
        }

        void ILifetimeMapping<LifetimeManager>.Map(Type lifetimeType, ILifetimeMapper<LifetimeManager> mapper)
        {
            Map(lifetimeType, mapper);
        }
    }

}
