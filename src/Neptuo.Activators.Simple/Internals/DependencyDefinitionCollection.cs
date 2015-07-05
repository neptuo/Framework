using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    public class DependencyDefinitionCollection : IDependencyDefinitionCollection
    {
        //TODO: Implement using registered features...
        public IDependencyDefinitionCollection Add(Type requiredType, DependencyLifetime lifetime, object target)
        {
            // Target is type to map to.
            Type targetType = target as Type;
            if (targetType != null)
            {
                if (requiredType.IsInterface)
                    throw new DependencyResolutionFailedException(String.Format("Target can't be interface. Mapping '{0}' to '{1}'.", requiredType.FullName, targetType.FullName));

                if (requiredType.IsAbstract)
                    throw new DependencyResolutionFailedException(String.Format("Target can't be abstract class. Mapping '{0}' to '{1}'.", requiredType.FullName, targetType.FullName));

                registry.Add(GetKey(requiredType), new DependencyDefinition(
                    requiredType,
                    lifetime,
                    FindBestConstructor(targetType)
                ));

                return this;
            }

            // Target is activator.
            IActivator<object> targetActivator = target as IActivator<object>;
            if (targetActivator != null)
            {
                instances.AddActivator(GetKey(requiredType), targetActivator);
                registry.Add(GetKey(requiredType), new DependencyDefinition(requiredType, lifetime, target));
                return this;
            }

            // Target is instance of required type.
            targetType = target.GetType();
            if (requiredType.IsAssignableFrom(targetType))
            {
                instances.AddObject(GetKey(requiredType), target);
                registry.Add(GetKey(requiredType), new DependencyDefinition(requiredType, lifetime, target));
                return this;
            }

            // Nothing else is supported.
            throw Ensure.Exception.InvalidOperation("Not supported target type '{0}'.", target.GetType().FullName);
        }

        public bool TryGet(Type requiredType, out IDependencyDefinition definition)
        {
            DependencyDefinition result;
            if (TryGetInternal(requiredType, out result))
            {
                definition = result;
                return true;
            }

            definition = null;
            return false;
        }

        internal bool TryGetInternal(Type requiredType, out DependencyDefinition definition)
        {
            throw new NotImplementedException();
        }

        private string GetKey(Type t)
        {
            return t.FullName;
        }

        private ConstructorInfo FindBestConstructor(Type type)
        {
            ConstructorInfo result = null;
            foreach (ConstructorInfo ctor in type.GetConstructors())
            {
                if (result == null)
                    result = ctor;
                else if (result.GetParameters().Length < ctor.GetParameters().Length)
                    result = ctor;
            }
            return result;
        }
    }
}
