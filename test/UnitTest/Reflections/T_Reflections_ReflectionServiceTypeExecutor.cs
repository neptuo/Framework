using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Reflections.Enumerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections
{
    [TestClass]
    public class T_Reflections_ReflectionServiceTypeExecutor
    {
        [TestMethod]
        public void BaseComposition()
        {
            StringBuilder result = new StringBuilder();

            IReflectionService reflectionService = ReflectionFactory.FromCurrentAppDomain();
            reflectionService.AddFilterTypeExecutor(true)
                .AddFilterNotInterface()
                .AddFilterNotAbstract()
                .AddHandler(t => result.AppendLine(t.FullName));

            File.WriteAllText("C:/Temp/Files.txt", result.ToString());
        }
    }
}
