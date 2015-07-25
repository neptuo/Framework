using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Converters;
using Neptuo.Converters.AutoExports;
using Neptuo.Reflections.Enumerators;
using Neptuo.Reflections.Enumerators.Executors;
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
            using (ITypeExecutorService executors = reflectionService.PrepareTypeExecutors())
            {
                executors.AddFiltered(true)
                    .AddFilterNotInterface()
                    .AddFilterNotAbstract()
                    .AddHandler(t => result.AppendLine(t.FullName));
            }

            File.WriteAllText("C:/Temp/Files.txt", result.ToString());
        }

        [TestMethod]
        public void InterfaceFiltering()
        {
            IReflectionService reflectionService = ReflectionFactory.FromCurrentAppDomain();

            int matchCount = 0;
            using (ITypeExecutorService executors = reflectionService.PrepareTypeExecutors())
            {
                executors.AddFiltered(false)
                    .AddFilterNotInterface()
                    .AddHandler(t =>
                    {
                        Assert.AreEqual(false, t.IsInterface);
                        matchCount++;
                    });
            }

            Ensure.Positive(matchCount, "matchCount");
        }

        [TestMethod]
        public void AbstractFiltering()
        {
            IReflectionService reflectionService = ReflectionFactory.FromCurrentAppDomain();

            int matchCount = 0;
            using (ITypeExecutorService executors = reflectionService.PrepareTypeExecutors())
            {
                executors.AddFiltered(false)
                    .AddFilterNotAbstract()
                    .AddHandler(t =>
                    {
                        Assert.AreEqual(false, t.IsAbstract);
                        matchCount++;
                    });
            }

            Ensure.Positive(matchCount, "matchCount");
        }

        [TestMethod]
        public void AttributeFiltering()
        {
            IReflectionService reflectionService = ReflectionFactory.FromCurrentAppDomain();

            int matchCount = 0;
            using (ITypeExecutorService executors = reflectionService.PrepareTypeExecutors())
            {
                executors.AddFiltered(false)
                    .AddFilterHasAttribute<ConverterAttribute>()
                    .AddHandler(t =>
                    {
                        Assert.AreEqual(typeof(IntToStringConverter), t);
                        matchCount++;
                    });
            }

            Assert.AreEqual(1, matchCount);
        }

        [TestMethod]
        public void AutoWireConverters()
        {
            IReflectionService reflectionService = ReflectionFactory.FromCurrentAppDomain();
            using (ITypeExecutorService executors = reflectionService.PrepareTypeExecutors())
            {
                executors.AddConverters();
            }

            string value;
            Assert.AreEqual(true, Converts.Repository.TryConvert<int, string>(5, out value));
            Assert.AreEqual("5", value);
        }
    }
}
