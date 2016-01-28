using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Metadata
{
    public partial class ReflectionCompositeTypeProvider : ICompositeTypeProvider
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

            Dictionary<int, ConstructorInfo> constructors = GetConstructors(type);
            IEnumerable<PropertyDescriptor> properties = GetProperties(type);

            List<CompositeVersion> versions = new List<CompositeVersion>();
            foreach (KeyValuePair<int, ConstructorInfo> constructor in constructors)
            {
                IEnumerable<PropertyDescriptor> versionProperties;

                // Create version from annotated properties.
                if (TryFindAnnotatedProperties(properties, constructor.Value.GetParameters().Length, constructor.Key, out versionProperties))
                {
                    versions.Add(BuildVersion(constructor.Key, constructor.Value, versionProperties));
                    continue;
                }

                // Create version from property name match.
                if(TryFindNamedProperties(properties, constructor.Value.GetParameters(), out versionProperties))
                {
                    versions.Add(BuildVersion(constructor.Key, constructor.Value, versionProperties));
                    continue;
                }

                throw new MismatchVersionConstructorException(type, constructor.Key);
            }

            CompositeProperty versionProperty = null;
            PropertyDescriptor versionPropertyDescriptor = properties.FirstOrDefault(p => p.PropertyInfo.GetCustomAttribute<CompositeVersionAttribute>() != null);
            if (versionPropertyDescriptor == null)
            {
                if (versions.Count == 1)
                    versionProperty = new CompositeProperty(0, "_Version", typeof(int), model => 1);
                else
                    throw new MissingVersionPropertyException(type);
            }
            else
            {
                Func<object, object> getter = delegateFactory.CreatePropertyGetter(versionPropertyDescriptor.PropertyInfo);

                // Use setter for version only when setter method is present and is public.
                Action<object, object> setter = null;
                if (versionPropertyDescriptor.PropertyInfo.CanWrite && versionPropertyDescriptor.PropertyInfo.SetMethod != null && versionPropertyDescriptor.PropertyInfo.SetMethod.IsPublic)
                    setter = delegateFactory.CreatePropertySetter(versionPropertyDescriptor.PropertyInfo);

                if (setter == null)
                    versionProperty = new CompositeProperty(0, versionPropertyDescriptor.PropertyInfo.Name, versionPropertyDescriptor.PropertyInfo.PropertyType, getter);
                else
                    versionProperty = new CompositeProperty(0, versionPropertyDescriptor.PropertyInfo.Name, versionPropertyDescriptor.PropertyInfo.PropertyType, getter, setter);
            }

            versions.Sort((v1, v2) => v1.Version.CompareTo(v2.Version));
            return new CompositeType(typeName, type, versions, versionProperty);
        }

        private bool TryFindAnnotatedProperties(IEnumerable<PropertyDescriptor> allProperties, int count, int version, out IEnumerable<PropertyDescriptor> versionProperties)
        {
            versionProperties = allProperties.Where(p => p.Attribute != null && p.Attribute.Version == version);
            return versionProperties.Count() == count;
        }

        private bool TryFindNamedProperties(IEnumerable<PropertyDescriptor> allProperties, ParameterInfo[] parameterInfos, out IEnumerable<PropertyDescriptor> versionProperties)
        {
            IEnumerable<string> propertyNames = parameterInfos.Select(p => p.Name.ToLowerInvariant());
            List<PropertyDescriptor> result = new List<PropertyDescriptor>();
            int index = 0;
            foreach (string propertyName in propertyNames)
            {
                PropertyDescriptor property = allProperties.FirstOrDefault(p => p.PropertyInfo.Name.ToLowerInvariant() == propertyName);
                if(property != null)
                {
                    result.Add(property);
                }

                index++;
            }

            if (result.Count == propertyNames.Count())
            {
                versionProperties = result;
                return true;
            }

            versionProperties = null;
            return false;
        }

        private CompositeVersion BuildVersion(int version, ConstructorInfo constructorInfo, IEnumerable<PropertyDescriptor> properties)
        {
            return new CompositeVersion(
                version,
                new CompositeConstructor(delegateFactory.CreateConstructorFactory(constructorInfo)),
                properties
                    .Select(p => new CompositeProperty(p.Attribute.Index, p.PropertyInfo.Name, p.PropertyInfo.PropertyType, delegateFactory.CreatePropertyGetter(p.PropertyInfo)))
                    .ToList()
            );
        }

        private Dictionary<int, ConstructorInfo> GetConstructors(Type type)
        {
            IEnumerable<ConstructorInfo> constructorInfos = type.GetConstructors();
            ConstructorInfo defaultConstructor = null;

            Dictionary<int, ConstructorInfo> constructors = new Dictionary<int, ConstructorInfo>();
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                CompositeConstructorAttribute constructorAttribute = constructorInfo.GetCustomAttribute<CompositeConstructorAttribute>();
                if (constructorAttribute == null)
                {
                    defaultConstructor = constructorInfo;
                }
                else
                {
                    constructors[constructorAttribute.Version] = constructorInfo;
                    defaultConstructor = null;
                }
            }

            if (defaultConstructor != null)
                constructors[1] = defaultConstructor;

            return constructors;
        }

        private IEnumerable<PropertyDescriptor> GetProperties(Type type)
        {
            List<PropertyDescriptor> properties = new List<PropertyDescriptor>();
            IEnumerable<PropertyInfo> propertyInfos = type.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                bool any = false;
                IEnumerable<CompositePropertyAttribute> attributes = propertyInfo.GetCustomAttributes<CompositePropertyAttribute>();
                foreach (CompositePropertyAttribute attribute in attributes)
                {
                    any = true;
                    properties.Add(new PropertyDescriptor()
                    {
                        PropertyInfo = propertyInfo,
                        Attribute = attribute
                    });
                }

                if (!any)
                {
                    properties.Add(new PropertyDescriptor()
                    {
                        PropertyInfo = propertyInfo,
                        Attribute = new CompositePropertyAttribute(1) 
                        { 
                            Version = 1
                        }
                    });
                }
            }

            return properties;
        }
    }
}
