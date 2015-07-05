using Neptuo.Activators.Internals;
using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Base reflection (and #kit compliant) implementation of <see cref="IDependencyContainer"/>.
    /// </summary>
    public class SimpleDependencyContainer : DisposableBase, IDependencyContainer
    {
        private readonly DependencyRegistry registry;
        private readonly InstanceStorage instances;
        private readonly string scopeName;

        public string ScopeName
        {
            get { return scopeName; }
        }
        
        /// <summary>
        /// Creates new intance.
        /// </summary>
        public SimpleDependencyContainer()
            : this(DependencyLifetime.RootScopeName, new DependencyRegistry(), new InstanceStorage())
        { }

        private SimpleDependencyContainer(string scopeName, DependencyRegistry registry, InstanceStorage instances)
        {
            Ensure.NotNullOrEmpty(scopeName, "scopeName");
            Ensure.NotNull(registry, "registry");
            Ensure.NotNull(instances, "instances");
            this.scopeName = scopeName;
            this.registry = registry;
            this.instances = instances;

            Add(typeof(IDependencyContainer), DependencyLifetime.NameScope(scopeName), this);
            Add(typeof(IDependencyProvider), DependencyLifetime.NameScope(scopeName), this);
        }

        //TODO: Implement using registered features...
        public IDependencyContainer Add(Type requiredType, DependencyLifetime lifetime, object target)
        {
            // Target is type to map to.
            Type targetType = target as Type;
            if (targetType != null)
            {
                if (requiredType.IsInterface)
                    throw new DependencyResolutionFailedException(String.Format("Target can't be interface. Mapping '{0}' to '{1}'.", requiredType.FullName, targetType.FullName));

                if (requiredType.IsAbstract)
                    throw new DependencyResolutionFailedException(String.Format("Target can't be abstract class. Mapping '{0}' to '{1}'.", requiredType.FullName, targetType.FullName));

                registry.Add(GetKey(requiredType), new DependencyRegistryItem(
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
                registry.Add(GetKey(requiredType), new DependencyRegistryItem(requiredType, lifetime));
                return this;
            }

            // Target is instance of required type.
            targetType = target.GetType();
            if (requiredType.IsAssignableFrom(targetType))
            {
                instances.AddObject(GetKey(requiredType), target);
                registry.Add(GetKey(requiredType), new DependencyRegistryItem(requiredType, lifetime));
                return this;
            }

            // Nothing else is supported.
            throw Ensure.Exception.InvalidOperation("Not supported target type '{0}'.", target.GetType().FullName);
        }

        #region IDependencyContainer

        public IDependencyDefinitionCollection Definitions
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDependencyProvider

        IDependencyDefinitionReadOnlyCollection IDependencyProvider.Definitions
        {
            get { throw new NotImplementedException(); }
        }

        public IDependencyContainer Scope(string scopeName)
        {
            return new SimpleDependencyContainer(
                scopeName, 
                new DependencyRegistry(registry.CopyRegistries()), 
                new InstanceStorage(
                    instances.CopyObjects(new List<string>()),
                    instances.CopyActivators(new List<string>())
                ) //TODO: SKip scoped instances.
            );
        }

        public object Resolve(Type requiredType)
        {
            string key = GetKey(requiredType);
            DependencyRegistryItem item = registry.GetByKey(key);
            if (item == null)
            {
                Add(requiredType, DependencyLifetime.Transient, requiredType);
                item = registry.GetByKey(key);
            }

            return Build(item);
        }

        #endregion

        private string GetKey(Type t)
        {
            return t.FullName;
        }

        private object Build(DependencyRegistryItem item)
        {
            throw new NotImplementedException();
            //if (item == null)
            //    return null;

            //if (item.Lifetime.IsTransient)
            //{
            //    if (item.HasConstructorInfo)
            //        return CreateInstance(item.ConstructorInfo);

            //    IActivator<object> activator = instances.TryGetActivator(GetKey(item.RequiredType));
            //    if (activator != null)
            //        return activator.Create();

            //    //TODO: What now?
            //}

            //object instance = item.Lifetime.GetValue();
            //if (instance != null)
            //    return instance;

            //if (item.ConstructorInfo == null)
            //    throw new DependencyResolutionFailedException("Missing constructor.");

            
            //item.Lifetime.SetValue(instance);
            //return instance;
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
