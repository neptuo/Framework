using Neptuo.PresentationModels.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.PresentationModels
{
    class TestFieldMetadataValidatorKey
    {
        public static void Test()
        {
            FieldMetadataValidatorKey key1 = new FieldMetadataValidatorKey("Person", "Name", "Required");
            Console.WriteLine(key1.GetHashCode());
            FieldMetadataValidatorKey key2 = new FieldMetadataValidatorKey("Person", "Surname", "Required");
            Console.WriteLine(key2.GetHashCode());
        }
    }
}
