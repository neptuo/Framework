using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Localization.GetText;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization
{
    [TestClass]
    public class T_Localization_Base
    {
        [TestMethod]
        public void DefaultHandler()
        {
            string text = (L)"Hello, World!";
            Assert.AreEqual("Hello, World!", text);
        }

        [TestMethod]
        public void TranslationCollection()
        {
            TranslationReaderCollection collection = new TranslationReaderCollection()
                .Add(new CultureInfo("cs-CZ"), new EmptyTranslationReader { Culture = new CultureInfo("cs-CZ") });

            ITranslationReader reader;
            Assert.AreEqual(true, collection.TryGetReader(new CultureInfo("cs-CZ"), Assembly.GetCallingAssembly(), out reader));
            Assert.AreEqual(false, collection.TryGetReader(new CultureInfo("en-US"), Assembly.GetCallingAssembly(), out reader));
        }
    }
}
