using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Metadata
{
    public class CompositeConstructor
    {
        public ConstructorInfo ConstructorInfo { get; private set; }

        public CompositeConstructor(ConstructorInfo constructorInfo)
        {
            Ensure.NotNull(constructorInfo, "constructorInfo");
            ConstructorInfo = constructorInfo;
        }
    }
}
