using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class InstanceResolver
    {
        private readonly DependencyDefinitionCollection definitions;
        private readonly InstanceStorage instances;

        public InstanceResolver(DependencyDefinitionCollection definitions, InstanceStorage instances)
        {
            Ensure.NotNull(definitions, "definitions");
            Ensure.NotNull(instances, "instances");
            this.definitions = definitions;
            this.instances = instances;
        }

        public object Resolve(Type requiredType)
        {
            DependencyDefinition definition;
            if (!definitions.TryGetInternal(requiredType, out definition))
            {
                definitions.Add(requiredType, DependencyLifetime.Transient, requiredType);
                if (!definitions.TryGetInternal(requiredType, out definition))
                    throw Ensure.Exception.InvalidOperation("Unnable to create registration for type '{0}'.", requiredType.FullName);
            }

            if (!definition.HasConstructorInfo)
                throw new NotImplementedException();

            return Build(definition);
        }

        private object CreateInstance(ConstructorInfo constructorInfo)
        {
            Ensure.NotNull(constructorInfo, "constructorInfo");

            object instance;
            ParameterInfo[] parameterDefinitions = constructorInfo.GetParameters();
            object[] parameters = new object[parameterDefinitions.Length];
            for (int i = 0; i < parameterDefinitions.Length; i++)
                parameters[i] = Resolve(parameterDefinitions[i].ParameterType);

            instance = constructorInfo.Invoke(parameters);
            return instance;
        }

        private object Build(DependencyDefinition definition)
        {
            //object instance = CreateInstance(definition.ConstructorInfo);
            //return instance;

            if (definition.Lifetime.IsTransient)
            {
                if (definition.HasConstructorInfo)
                    return CreateInstance(definition.ConstructorInfo);

                // When mapped to the instance of activator.
                IActivator<object> activator = instances.TryGetActivator(definition.Key);
                if (activator != null)
                    return activator.Create();

                // When mapped to the type of activator.
                //TODO: Implement.

                // When mapped to the implementation type.
                object instance = instances.TryGetObject(definition.Key);
                if(instance == null)
                    return CreateInstance(definition.ConstructorInfo);



                //TODO: What now?
            }

            throw new NotImplementedException();
        }
    }
}
