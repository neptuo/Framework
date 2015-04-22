using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
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

        protected override string BuildModelIdentifier()
        {
            return BuildIdentifier(ModelType);
        }

        protected override IEnumerable<IFieldDefinition> BuildFieldDefinitions()
        {
            List<IFieldDefinition> fields = new List<IFieldDefinition>();
            foreach (PropertyInfo propertyInfo in ModelType.GetProperties())
                fields.Add(new FieldDefinition(BuildIdentifier(propertyInfo), propertyInfo.PropertyType, BuildMetadata(propertyInfo)));

            return fields;
        }

        protected override IKeyValueCollection BuildModelMetadata()
        {
            return BuildMetadata(ModelType);
        }

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

        protected virtual string BuildIdentifier(MemberInfo memberInfo)
        {
            return memberInfo.Name;
        }
    }
}
