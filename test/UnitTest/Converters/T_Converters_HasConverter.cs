using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    [TestClass]
    public class T_Converters_HasConverter
    {
        private static bool TryGetConverter(ConverterSearchContext context, out IConverter converter)
        {
            if (context.SourceType == typeof(string) && context.TargetType == typeof(TimeSpan))
            {
                converter = new TimeSpanConveter();
                return true;
            }

            converter = null;
            return false;
        }

        private static bool TryParseWithContext(IConverterContext<string> context, out DateTime dateTime)
        {
            return DateTime.TryParse(context.SourceValue, out dateTime);
        }

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            TestConvertersBase.ClearRepository();

            Converts.Repository
                .Add<string, int>(Int32.TryParse)
                .AddSearchHandler(TryGetConverter)
                .Add<IConverterContext<string>, DateTime>(TryParseWithContext);
        }

        [TestMethod]
        public void Generic()
        {
            Assert.AreEqual(true, Converts.Repository.HasConverter<string, int>());
        }

        [TestMethod]
        public void GenericSearchHandler()
        {
            Assert.AreEqual(true, Converts.Repository.HasConverter<string, TimeSpan>());
        }

        [TestMethod]
        public void GenericContext()
        {
            Assert.AreEqual(true, Converts.Repository.HasConverter<string, DateTime>());
        }

        [TestMethod]
        public void GenericMissing()
        {
            Assert.AreEqual(false, Converts.Repository.HasConverter<string, double>());
        }

        [TestMethod]
        public void NonGeneric()
        {
            Assert.AreEqual(true, Converts.Repository.HasConverter(typeof(string), typeof(int)));
        }

        [TestMethod]
        public void NonGenericSearchHandler()
        {
            Assert.AreEqual(true, Converts.Repository.HasConverter(typeof(string), typeof(TimeSpan)));
        }

        [TestMethod]
        public void NonGenericContext()
        {
            Assert.AreEqual(true, Converts.Repository.HasConverter(typeof(string), typeof(DateTime)));
        }

        [TestMethod]
        public void NonGenericMissing()
        {
            Assert.AreEqual(false, Converts.Repository.HasConverter(typeof(string), typeof(double)));
        }
    }
}
