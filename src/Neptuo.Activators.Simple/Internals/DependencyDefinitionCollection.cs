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
        private readonly string scopeName;
        private readonly InstanceStorage instances;
        private readonly DependencyDefinitionCollection parentCollection;

        public DependencyDefinitionCollection(string scopeName, InstanceStorage instances)
        {
            Ensure.NotNull(instances, "instances");
            this.scopeName = scopeName;
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
                    AddDefinition(definition);
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
                if (requiredType.IsInterface)
                    throw new DependencyResolutionFailedException(String.Format("Target can't be interface. Mapping '{0}' to '{1}'.", requiredType.FullName, targetType.FullName));

                if (requiredType.IsAbstract)
                    throw new DependencyResolutionFailedException(String.Format("Target can't be abstract class. Mapping '{0}' to '{1}'.", requiredType.FullName, targetType.FullName));

                DependencyDefinition definition = new DependencyDefinition(
                    requiredType,
                    lifetime,
                    targetType,
                    FindBestConstructor(targetType)
                );
                AddDefinition(definition);

                return this;
            }

            // Target is activator.
            IActivator<object> targetActivator = target as IActivator<object>;
            if (targetActivator != null)
            {
                DependencyDefinition definition = new DependencyDefinition(
                    requiredType,
                    lifetime,
                    targetType
                );
                instances.AddActivator(definition.Key, targetActivator);
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
                    targetType
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
            if (definition.Lifetime.IsTransient || (definition.Lifetime.IsScoped && (definition.Lifetime.IsNamed && definition.Lifetime.Name == scopeName) || !definition.Lifetime.IsNamed))
                definitionByKey[definition.Key] = definition;

            // If definition is targeted for other scope, add it to the definitionByScopeName.
            if(definition.Lifetime.IsNamed)
            {
                List<DependencyDefinition> list;
                if (!definitionByScopeName.TryGetValue(definition.Lifetime.Name, out list))
                    definitionByScopeName[definition.Lifetime.Name] = list = new List<DependencyDefinition>();

                list.Add(definition);
            }
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
            return definitionByKey.TryGetValue(key, out definition);
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
        /// Tries to return all definitions bound to the <paramref name="scopeName"/>.
        /// </summary>
        /// <param name="scopeName">Scope name to find definitions for.</param>
        /// <param name="definitions">Enumerations for scoped definitions for <paramref name="scopeName"/>.</param>
        /// <returns><c>true</c> if such definitions exits (at least one); <c>false</c> otherwise.</returns>
        internal bool TryGetChild(string scopeName, out IEnumerable<DependencyDefinition> definitions)
        {
            List<DependencyDefinition> result;
            if (definitionByScopeName.TryGetValue(scopeName, out result))
            {
                definitions = result;

                IEnumerable<DependencyDefinition> parentResult;
                if (parentCollection != null && parentCollection.TryGetChild(scopeName, out parentResult))
                    definitions = Enumerable.Concat(result, parentResult);

                return true;
            }

            if (parentCollection != null)
                return parentCollection.TryGetChild(scopeName, out definitions);

            definitions = null;
            return false;
        }
    }
}
