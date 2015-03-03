using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class TargetMapper
    {
        public void Register(IUnityContainer unityContainer, Type requiredType, LifetimeManager lifetimeManager, object target)
        {
            Type targetType = target as Type;
            if (targetType != null)
            {
                unityContainer.RegisterType(requiredType, targetType, lifetimeManager);
                return;
            }


        }
    }
}
