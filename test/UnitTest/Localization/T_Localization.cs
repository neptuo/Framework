using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Localization.GetText;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization
{
    [TestClass]
    public class T_Localization
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

        [TestMethod]
        public void PlainTextReaderActivator()
        {
            PlainTextTranslationReaderFactory factory = new PlainTextTranslationReaderFactory();
            using (MemoryStream fileContent = new MemoryStream())
            using (TextWriter writer = new StreamWriter(fileContent))
            {
                writer.WriteLine("Hello, World!=Ahoj světe!");
                writer.WriteLine("Character '\\=' means 'equal'=Znak '\\=' znamená 'rovnost'");
                writer.Flush();
                fileContent.Seek(0, SeekOrigin.Begin);

                ITranslationReader reader = factory.Create(fileContent);

                string translatedText;
                Assert.AreEqual(true, reader.TryGet("Hello, World!", out translatedText));
                Assert.AreEqual("Ahoj světe!", translatedText);
                Assert.AreEqual(true, reader.TryGet("Character '\\=' means 'equal'", out translatedText));
                Assert.AreEqual("Znak '\\=' znamená 'rovnost'", translatedText);
            }
        }

        [TestMethod]
        public void TranslateTextUsingL()
        {
            PlainTextTranslationReaderFactory factory = new PlainTextTranslationReaderFactory();
            using (MemoryStream fileContent = new MemoryStream())
            using (TextWriter writer = new StreamWriter(fileContent))
            {
                writer.WriteLine("Hello, World!=Ahoj světe!");
                writer.WriteLine("Character '\\=' means 'equal'=Znak '\\=' znamená 'rovnost'");
                writer.Flush();
                fileContent.Seek(0, SeekOrigin.Begin);

                ITranslationReader reader = factory.Create(fileContent);
                TranslationAdapter adapter = new TranslationAdapter(new DefaultCultureProvider(), new TranslationReaderCollection().Add(new CultureInfo("cs"), reader));
                Translate.SetHandler(adapter.Translate);

                Assert.AreEqual("Ahoj světe!", (L)"Hello, World!");
            }
        }
    }
}
