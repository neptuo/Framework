using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    public class TestConvertersBase
    {
        public static void ClearRepository()
        {
            var fieldInfo = typeof(Converts).GetField("repository", BindingFlags.Static | BindingFlags.NonPublic);
            Assert.IsNotNull(fieldInfo);
            fieldInfo.SetValue(null, null);
        }

        [TestInitialize]
        public void Initialize()
            => ClearRepository();
    }
}
