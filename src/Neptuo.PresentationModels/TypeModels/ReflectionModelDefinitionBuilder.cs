using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    /// <summary>
    /// Reflection based builder for <see cref="IModelDefinition"/>.
    /// - Fields are created from properties.
    /// - Metadata are created from attributes.
    /// </summary>
    public class ReflectionModelDefinitionBuilder : ModelDefinitionBuilderBase
    {
        public Type ModelType { get; private set; }
        public AttributeMetadataReaderCollection MetadataReaderCollection { get; private set; }

        public ReflectionModelDefinitionBuilder(Type modelType, AttributeMetadataReaderCollection metadataReaderCollection)
        {
            Ensure.NotNull(modelType, "modelType");
            Ensure.NotNull(metadataReaderCollection, "metadataReaderCollection");
            ModelType = modelType;
            MetadataReaderCollection = metadataReaderCollection;
        }

        /// <summary>
        /// Returns identifier for model definition.
        /// Calls <see cref="ReflectionModelDefinitionBuilder.BuildIdentifier"/> with <see cref="ReflectionModelDefinitionBuilder.ModelType"/>.
        /// </summary>
        /// <returns>Identifier for model definition.</returns>
        protected override string BuildModelIdentifier()
        {
            return BuildIdentifier(ModelType);
        }

        /// <summary>
        /// Returns metadata for model definition.
        /// Calls <see cref="ReflectionModelDefinitionBuilder.BuildMetadata"/> with <see cref="ReflectionModelDefinitionBuilder.ModelType"/>.
        /// </summary>
        /// <returns>Metadata for model definition.</returns>
        protected override IKeyValueCollection BuildModelMetadata()
        {
            return BuildMetadata(ModelType);
        }

        /// <summary>
        /// Emurates over public properties and for each one generates field.
        /// </summary>
        /// <returns>Enumeration of fields.</returns>
        protected override IEnumerable<IFieldDefinition> BuildFieldDefinitions()
        {
            List<IFieldDefinition> fields = new List<IFieldDefinition>();
            foreach (PropertyInfo propertyInfo in ModelType.GetProperties())
            {
                if (IsFieldCompatibleProperty(propertyInfo))
                    fields.Add(new FieldDefinition(BuildIdentifier(propertyInfo), BuildFieldType(propertyInfo), BuildMetadata(propertyInfo)));
            }

            return fields;
        }

        /// <summary>
        /// Determines whether to create field definition from <paramref name="propertyInfo"/>.
        /// </summary>
        /// <param name="propertyInfo">Property info.</param>
        /// <returns><c>true</c> if field definition should be created from <paramref name="propertyInfo"/>; <c>false</c> otherwise.</returns>
        protected virtual bool IsFieldCompatibleProperty(PropertyInfo propertyInfo)
        {
            return true;
        }

        /// <summary>
        /// Reads attributes of <paramref name="memberInfo"/> and tries to create metadata for each one.
        /// </summary>
        /// <param name="memberInfo">Reflection source.</param>
        /// <returns>Collection of metadata values.</returns>
        protected virtual IKeyValueCollection BuildMetadata(MemberInfo memberInfo)
        {
            MetadataCollection collection = new MetadataCollection();
            foreach (Attribute attribute in memberInfo.GetCustomAttributes(true))
            {
                IMetadataReader reader = attribute as IMetadataReader;
                if (reader != null)
                {
                    reader.Apply(collection);
                }
                else
                {
                    IAttributeMetadataReader attributeReader;
                    if (MetadataReaderCollection.TryGet(attribute.GetType(), out attributeReader))
                        attributeReader.Apply(attribute, collection);
                }
            }
            return collection;
        }

        /// <summary>
        /// Returns identifier for <paramref name="memberInfo"/>.
        /// </summary>
        /// <param name="memberInfo">Reflection source.</param>
        /// <returns>Identifier for <paramref name="memberInfo"/>.</returns>
        protected virtual string BuildIdentifier(MemberInfo memberInfo)
        {
            return memberInfo.Name;
        }

        /// <summary>
        /// Returns field type for field from <paramref name="propertyInfo"/>.
        /// </summary>
        /// <param name="propertyInfo">Reflection source.</param>
        /// <returns>Field type for field from <paramref name="propertyInfo"/>.</returns>
        protected virtual Type BuildFieldType(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType;
        }
    }
}
