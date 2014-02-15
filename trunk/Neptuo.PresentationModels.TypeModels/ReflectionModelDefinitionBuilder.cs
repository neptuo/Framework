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
        public MetadataReaderService MetadataReaderService { get; private set; }

        public ReflectionModelDefinitionBuilder(Type modelType, MetadataReaderService metadataReaderService)
        {
            if (modelType == null)
                throw new ArgumentNullException("modelType");

            if (metadataReaderService == null)
                throw new ArgumentNullException("metadataReaderService");

            ModelType = modelType;
            MetadataReaderService = metadataReaderService;
        }

        protected override string BuildModelIdentifier()
        {
            return BuildIdentifier(ModelType);
        }

        protected override IEnumerable<IFieldDefinition> BuildFieldDefinitions()
        {
            List<IFieldDefinition> fields = new List<IFieldDefinition>();
            foreach (PropertyInfo propertyInfo in ModelType.GetProperties())
                fields.Add(new FieldDefinition(BuildIdentifier(propertyInfo), new TypeFieldType(propertyInfo.PropertyType), BuildMetadata(propertyInfo)));

            return fields;
        }

        protected override IModelMetadataCollection BuildModelMetadata()
        {
            return BuildMetadata(ModelType);
        }

        protected virtual MetadataCollection BuildMetadata(MemberInfo memberInfo)
        {
            MetadataBuilderCollection collection = new MetadataBuilderCollection();
            foreach (Attribute attribute in memberInfo.GetCustomAttributes(true))
            {
                IMetadataReader reader = attribute as IMetadataReader;
                if (reader != null)
                    reader.Apply(attribute, collection);
                else if(MetadataReaderService.TryGetReader(attribute.GetType(), out reader))
                    reader.Apply(attribute, collection);
            }
            return collection;
        }

        protected virtual string BuildIdentifier(MemberInfo memberInfo)
        {
            return memberInfo.Name;
        }
    }
}
