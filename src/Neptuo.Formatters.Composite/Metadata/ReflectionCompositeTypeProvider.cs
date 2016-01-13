﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Metadata
{
    public class ReflectionCompositeTypeProvider : ICompositeTypeProvider
    {
        private readonly Dictionary<Type, CompositeType> storageByType = new Dictionary<Type, CompositeType>();
        private readonly Dictionary<string, CompositeType> storageByName = new Dictionary<string, CompositeType>();
        private readonly ICompositeDelegateFactory delegateFactory;

        public ReflectionCompositeTypeProvider(ICompositeDelegateFactory delegateFactory)
        {
            Ensure.NotNull(delegateFactory, "delegateFactory");
            this.delegateFactory = delegateFactory;
        }

        #region ICompositeTypeProvider

        public bool TryGet(Type type, out CompositeType definition)
        {
            Ensure.NotNull(type, "type");
            if (storageByType.TryGetValue(type, out definition))
                return true;

            definition = BuildType(type);
            if (definition == null)
                return false;

            storageByType[type] = definition;
            storageByName[definition.Name] = definition;
            return true;
        }

        public bool TryGet(string typeName, out CompositeType definition)
        {
            Ensure.NotNullOrEmpty(typeName, "typeName");
            return storageByName.TryGetValue(typeName, out definition);
        }

        #endregion

        private CompositeType BuildType(Type type)
        {
            string typeName = type.FullName;

            CompositeTypeAttribute typeAttribute = type.GetCustomAttribute<CompositeTypeAttribute>();
            if (typeAttribute != null)
                typeName = typeAttribute.Name;

            IEnumerable<ConstructorInfo> constructorInfos = type.GetConstructors();
            IEnumerable<PropertyInfo> propertyInfos = type.GetProperties();
            CompositeProperty versionProperty = null;

            // Composite properties by version.
            Dictionary<int, List<CompositeProperty>> properties = new Dictionary<int, List<CompositeProperty>>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                IEnumerable<CompositePropertyAttribute> propertyAttributes = propertyInfo.GetCustomAttributes<CompositePropertyAttribute>();
                foreach (CompositePropertyAttribute propertyAttribute in propertyAttributes)
                {
                    List<CompositeProperty> versionProperties;
                    if (!properties.TryGetValue(propertyAttribute.Version, out versionProperties))
                        properties[propertyAttribute.Version] = versionProperties = new List<CompositeProperty>();

                    versionProperties.Add(new CompositeProperty(propertyAttribute.Index, delegateFactory.CreatePropertyGetter(propertyInfo)));
                }

                CompositeVersionAttribute versionAttribute = propertyInfo.GetCustomAttribute<CompositeVersionAttribute>();
                if (versionAttribute != null)
                {
                    Func<object, object> getter = delegateFactory.CreatePropertyGetter(propertyInfo);

                    // Use setter for version only when setter method is present and is public.
                    Action<object, object> setter = null;
                    if (propertyInfo.CanWrite && propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsPublic)
                        setter = delegateFactory.CreatePropertySetter(propertyInfo);

                    if (setter == null)
                        versionProperty = new CompositeProperty(0, getter);
                    else
                        versionProperty = new CompositeProperty(0, getter, setter);
                }
            }

            if(versionProperty == null)
                throw new MissingVersionPropertyException(type);

            // Constructors by version.
            Dictionary<int, CompositeConstructor> constructors = new Dictionary<int, CompositeConstructor>();
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                CompositeConstructorAttribute constructorAttribute = constructorInfo.GetCustomAttribute<CompositeConstructorAttribute>();
                if (constructorAttribute != null)
                    constructors[constructorAttribute.Version] = new CompositeConstructor(delegateFactory.CreateConstructorFactory(constructorInfo));
            }

            List<CompositeVersion> versions = new List<CompositeVersion>();
            foreach (KeyValuePair<int, List<CompositeProperty>> versionProperties in properties)
            {
                CompositeConstructor versionConstructor;
                if (!constructors.TryGetValue(versionProperties.Key, out versionConstructor))
                    throw new MissingVersionConstructorException(type, versionProperties.Key);

                CompositeVersion version = new CompositeVersion(versionProperties.Key, versionConstructor, versionProperties.Value);
                versions.Add(version);
            }

            versions.Sort((v1, v2) => v1.Version.CompareTo(v2.Version));
            return new CompositeType(typeName, type, versions, versionProperty);
        }
    }
}
