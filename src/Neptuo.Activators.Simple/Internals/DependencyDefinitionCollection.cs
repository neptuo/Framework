using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class DependencyDefinitionCollection : IDependencyDefinitionCollection
    {
        private readonly Dictionary<string, DependencyDefinition> definitionByKey = new Dictionary<string, DependencyDefinition>();
        private readonly Dictionary<string, List<DependencyDefinition>> definitionByScopeName = new Dictionary<string, List<DependencyDefinition>>();
        private readonly InstanceStorage instances;
        private readonly DependencyDefinitionCollection parentCollection;

        public string ScopeName { get; private set; }

        public DependencyDefinitionCollection(string scopeName, InstanceStorage instances)
        {
            Ensure.NotNull(instances, "instances");
            ScopeName = scopeName;
            this.instances = instances;
        }

        public DependencyDefinitionCollection(string scopeName, InstanceStorage instances, DependencyDefinitionCollection parentCollection)
            : this(scopeName, instances)
        {
            Ensure.NotNull(parentCollection, "parentCollection");
            this.parentCollection = parentCollection;

            IEnumerable<DependencyDefinition> definitions;
            if(parentCollection.TryGetChild(scopeName, out definitions))
            {
                foreach (DependencyDefinition definition in definitions)
                    Add(definition.RequiredType, definition.Lifetime, definition.Target);
            }
        }

        //TODO: Implement using registered features...
        public IDependencyDefinitionCollection Add(Type requiredType, DependencyLifetime lifetime, object target)
        {
            Ensure.NotNull(requiredType, "requiredType");
            Ensure.NotNull(target, "target");

            // Target is type to map to.
            Type targetType = target as Type;
            if (targetType != null)
            {
                if (targetType.IsInterface)
                    throw new DependencyRegistrationFailedException(String.Format("Target can't be interface. Mapping '{0}' to '{1}'.", requiredType.FullName, targetType.FullName));

                if (targetType.IsAbstract)
                    throw new DependencyRegistrationFailedException(String.Format("Target can't be abstract class. Mapping '{0}' to '{1}'.", requiredType.FullName, targetType.FullName));

                ConstructorInfo constructorInfo = FindBestConstructor(targetType);
                if(constructorInfo == null)
                    throw new DependencyRegistrationFailedException(String.Format("Target must has public contructor. Mapping '{0}' to '{1}'.", requiredType.FullName, targetType.FullName));

                DependencyDefinition definition = new DependencyDefinition(
                    requiredType,
                    lifetime,
                    targetType,
                    constructorInfo
                );
                FindDependencyProperties(definition);
                AddDefinition(definition);

                return this;
            }

            // Target is activator.
            IFactory<object> targetActivator = target as IFactory<object>;
            if (targetActivator != null)
            {
                DependencyDefinition definition = new DependencyDefinition(
                    requiredType,
                    lifetime,
                    targetActivator
                );
                instances.AddFactory(definition.Key, targetActivator);
                AddDefinition(definition);
                return this;
            }

            // Target is instance of required type.
            targetType = target.GetType();
            if (requiredType.IsAssignableFrom(targetType))
            {
                DependencyDefinition definition = new DependencyDefinition(
                    requiredType,
                    lifetime,
                    target
                );
                instances.AddObject(definition.Key, target);
                AddDefinition(definition);
                return this;
            }

            // Nothing else is supported.
            throw Ensure.Exception.InvalidOperation("Not supported target type '{0}'.", target.GetType().FullName);
        }

        private void AddDefinition(DependencyDefinition definition)
        {
            // If definition is usable for current scope, add it to the definitionByKey.
            if (definition.Lifetime.IsTransient || (definition.Lifetime.IsScoped && (definition.Lifetime.IsNamed && definition.Lifetime.Name == ScopeName) || !definition.Lifetime.IsNamed))
                definitionByKey[definition.Key] = definition;

            if(definition.Lifetime.IsNamed)
            {
                // If definition is targeted for other scope, add it to the definitionByScopeName.
                AddDefinitionToScope(definition.Lifetime.Name, definition);
            } 
            else if(definition.Lifetime.IsScoped)
            {
                // If definition is targeted to any scope, add it to the definitionByScopeName with empty scope name.
                AddDefinitionToScope(String.Empty, definition);
            }
        }

        private void AddDefinitionToScope(string scopeName, DependencyDefinition definition)
        {
            List<DependencyDefinition> list;
            if (!definitionByScopeName.TryGetValue(scopeName, out list))
                definitionByScopeName[scopeName] = list = new List<DependencyDefinition>();

            list.Add(definition);
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
            string key = requiredType.FullName;
            if (definitionByKey.TryGetValue(key, out definition))
            {
                definition = definition.Clone(true);
                return true;
            }

            foreach (List<DependencyDefinition> items in definitionByScopeName.Values)
            {
                foreach (DependencyDefinition item in items)
                {
                    if(item.RequiredType == requiredType)
                    {
                        definition = item.Clone(false);
                        return true;
                    }
                }
            }

            if (parentCollection != null)
            {
                if (parentCollection.TryGetInternal(requiredType, out definition))
                    return true;
            }

            definition = null;
            return false;
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

        /// <summary>
        /// Tries to find dependency properties on implementation type.
        /// </summary>
        /// <param name="definition">Dependency definition.</param>
        private void FindDependencyProperties(DependencyDefinition definition)
        {
            Type targetType = definition.Target as Type;
            if(targetType == null)
                return;

            foreach (PropertyInfo property in targetType.GetProperties())
            {
                if (property.GetCustomAttributes(typeof(DependencyAttribute)).Any())
                    definition.DependencyProperties.Add(property);
            }
        }

        /// <summary>
        /// Tries to return all definitions bound to the <paramref name="scopeName"/>.
        /// </summary>
        /// <param name="scopeName">Scope name to find definitions for.</param>
        /// <param name="definitions">Enumerations for scoped definitions for <paramref name="scopeName"/>.</param>
        /// <returns><c>true</c> if such definitions exits (at least one); <c>false</c> otherwise.</returns>
        internal bool TryGetChild(string scopeName, out IEnumerable<DependencyDefinition> definitions)
        {
            bool isSucess = false;
            definitions = Enumerable.Empty<DependencyDefinition>();

            // 1) Find registrations from parent.
            if (parentCollection != null)
            {
                IEnumerable<DependencyDefinition> parentResult;
                if (parentCollection.TryGetChild(String.Empty, out parentResult))
                {
                    definitions = Enumerable.Concat(definitions, parentResult);
                    isSucess = true;
                }

                if (parentCollection.TryGetChild(scopeName, out parentResult))
                {
                    definitions = Enumerable.Concat(definitions, parentResult);
                    isSucess = true;
                }
            }

            // 2) Find registrations from current.
            List<DependencyDefinition> result;
            if (definitionByScopeName.TryGetValue(String.Empty, out result))
            {
                definitions = Enumerable.Concat(definitions, result);
                isSucess = true;
            }

            if (definitionByScopeName.TryGetValue(scopeName, out result))
            {
                definitions = Enumerable.Concat(definitions, result);
                isSucess = true;
            }

            if (isSucess)
                return true;

            definitions = null;
            return false;
        }
    }
}
