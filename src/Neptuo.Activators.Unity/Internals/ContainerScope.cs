using Microsoft.Practices.Unity;
using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class ContainerScope : DisposableBase
    {
        public ContainerScope ParentScope { get; private set; }
        public IUnityContainer UnityContainer { get; private set; }
        public string Name { get; private set; }

        public ContainerScope(IUnityContainer unityContainer)
            : this(null, null, unityContainer)
        { }

        private ContainerScope(ContainerScope parentScope, string name, IUnityContainer unityContainer)
        {
            Guard.NotNull(unityContainer, "unityContainer");
            ParentScope = parentScope;
            Name = name;
            UnityContainer = unityContainer;
        }

        public ContainerScope CreateScope(string name)
        {
            return new ContainerScope(this, name, new UnityContainer());
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            UnityContainer.Dispose();
        }
    }
}
