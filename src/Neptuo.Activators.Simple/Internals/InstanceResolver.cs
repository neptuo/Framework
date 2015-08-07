using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    /// <summary>
    /// Component that provides logic for creating or reusing instances.
    /// </summary>
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

            if (definition.IsResolvable)
                return Build(definition);

            throw Ensure.Exception.NotResolvable(requiredType);
        }

        private object Build(DependencyDefinition definition)
        {
            // For transient definition, create new instance (always).
            if (definition.Lifetime.IsTransient)
                return CreateNewInstanceFromDefinition(definition);

            // For scoped definition, try get instance or create new one.
            object instance = instances.TryGetObject(definition.Key);
            if (instance == null)
            {
                instance = CreateNewInstanceFromDefinition(definition);
                instances.AddObject(definition.Key, instance);
            }

            return instance;
        }

        private object CreateNewInstanceFromDefinition(DependencyDefinition definition)
        {
            // When mapped to the implementation type or itself (type).
            if (definition.HasConstructorInfo)
            {
                object instance = CreateInstance(definition.ConstructorInfo);
                FillDependencyProperties(definition, instance);
                return instance;
            }

            // When mapped to the instance of activator.
            IFactory<object> activator = instances.TryGetFactory(definition.Key);
            if (activator != null)
                return activator.Create();

            // When mapped to the type of activator.
            //TODO: Implement.
            throw new NotImplementedException();
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

        private void FillDependencyProperties(DependencyDefinition definition, object instance)
        {
            foreach (PropertyInfo propertyInfo in definition.DependencyProperties)
                propertyInfo.SetValue(instance, Resolve(propertyInfo.PropertyType));
        }
    }
}
