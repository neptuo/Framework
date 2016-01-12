using System;
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

                    versionProperties.Add(new CompositeProperty(propertyAttribute.Index, propertyInfo));
                }
            }

            // Constructors by version.
            Dictionary<int, CompositeConstructor> constructors = new Dictionary<int, CompositeConstructor>();
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                CompositeConstructorAttribute constructorAttribute = constructorInfo.GetCustomAttribute<CompositeConstructorAttribute>();
                if (constructorAttribute != null)
                    constructors[constructorAttribute.Version] = new CompositeConstructor(constructorInfo);
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

            return new CompositeType(typeName, type, versions);
        }
    }
}
