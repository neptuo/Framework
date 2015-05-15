using Neptuo.FileSystems;
using Neptuo.PresentationModels;
using Neptuo.PresentationModels.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.PresentationModels
{
    class TestXmlModelDefinition
    {
        public static void Test()
        {
            IReadOnlyFile xmlFile = LocalFileSystem.FromFilePath("../../PresentationModels/Person.xml");

            XmlTypeMappingCollection typeMappings = new XmlTypeMappingCollection().AddStandartKeywords();
            XmlModelDefinitionBuilder builder = new XmlModelDefinitionBuilder(typeMappings, xmlFile);
            IModelDefinition modelDefiniton = builder.Create();

        }
    }
}
