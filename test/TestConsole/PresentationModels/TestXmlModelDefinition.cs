using Neptuo.FileSystems;
using Neptuo.FileSystems.Features;
using Neptuo.Models.Features;
using Neptuo.PresentationModels;
using Neptuo.PresentationModels.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.PresentationModels
{
    class TestXmlModelDefinition
    {
        public static void Test()
        {
            IFile personXmlFile = LocalFileSystem.FromFilePath("../../PresentationModels/PersonDefinition.xml");
            IFile organizationXmlFile = LocalFileSystem.FromFilePath("../../PresentationModels/OrganizationDefinition.xml");

            XmlTypeMappingCollection typeMappings = new XmlTypeMappingCollection().AddStandartKeywords();
            IModelDefinition personDefiniton = new XmlModelDefinitionBuilder(typeMappings, new FileContentFactory(personXmlFile)).Create();
            IModelDefinition organizationDefinition = new XmlModelDefinitionBuilder(typeMappings, new FileContentFactory(organizationXmlFile)).Create();

            XmlModelDefinitionSerializer serializer = new XmlModelDefinitionSerializer(typeMappings);
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(personDefiniton, writer);
                Console.WriteLine(writer);
            }

            IFile mixedXmlFile = LocalFileSystem.FromFilePath("../../PresentationModels/MixedDataSource.xml");
            XmlModelValueGetterFactory getterFactory = new XmlModelValueGetterFactory(mixedXmlFile.With<IFileContentReader>().GetContentAsStream());

            XmlModelValueGetterCollection persons = getterFactory.Create(personDefiniton);
            XmlModelValueGetterCollection organizations = getterFactory.Create(organizationDefinition);

            Console.WriteLine("---------------------");
            foreach (IModelValueGetter getter in persons)
            {
                Console.WriteLine(
                    "Person: ({0}) {1}, {2}",
                    getter.GetValueOrDefault("ID", -1),
                    getter.GetValueOrDefault("FirstName", String.Empty),
                    getter.GetValueOrDefault("LastName", String.Empty)
                );
            }

            foreach (IModelValueGetter getter in organizations)
            {
                Console.WriteLine(
                    "Organization: ({0}) {1}",
                    getter.GetValueOrDefault("ID", -1),
                    getter.GetValueOrDefault("Name", String.Empty)
                );
            }

            XmlModelValueSetterFactory setterFactory = new XmlModelValueSetterFactory("DataSource");

            CopyModelValueProvider personCopier = new CopyModelValueProvider(personDefiniton, false);
            foreach (IModelValueGetter getter in persons)
                personCopier.Update(setterFactory.Create(personDefiniton), getter);

            CopyModelValueProvider organizationCopier = new CopyModelValueProvider(organizationDefinition, false);
            foreach (IModelValueGetter getter in organizations)
                organizationCopier.Update(setterFactory.Create(organizationDefinition), getter);

            //IFile newMixedFile = (IFile)LocalFileSystem.FromFilePath("../../PresentationModels/MixedDataSourceNEW.xml");
            using (FileStream stream = new FileStream("../../PresentationModels/MixedDataSourceNEW.xml", FileMode.OpenOrCreate))
            {
                setterFactory.SaveToStream(stream);
            }

        }
    }
}
