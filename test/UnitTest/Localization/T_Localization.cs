using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Globalization;
using Neptuo.Localization.Readers;
using Neptuo.Localization.Readers.Factories;
using Neptuo.Localization.Readers.Providers;
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
                .Add(new CultureInfo("cs-CZ"), new EmptyTranslationReader());

            ITranslationReader reader;
            Assert.AreEqual(true, collection.TryGetReader(new CultureInfo("cs-CZ"), Assembly.GetCallingAssembly(), out reader));
            Assert.AreEqual(false, collection.TryGetReader(new CultureInfo("en-US"), Assembly.GetCallingAssembly(), out reader));
        }

        [TestMethod]
        public void PlainTextReaderFactory()
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

        [TestMethod]
        public void DirectoryProviderFactory()
        {
            DirectoryTranslationReaderProviderFactory factory = new DirectoryTranslationReaderProviderFactory(
                new PlainTextTranslationReaderFactory(), 
                "*.txt"
            );

            string rootPath = @"C:\Temp2\Localization2";
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            File.WriteAllText(Path.Combine(rootPath, "en-US.txt"), "Hello, World!=Hello, World!");
            File.WriteAllText(Path.Combine(rootPath, "cs-CZ.txt"), "Hello, World!=Ahoj všichni!");
            File.WriteAllText(Path.Combine(rootPath, "UnitTest.cs-CZ.txt"), "Hello, World!=Ahoj!");


            /// EN ------------------
            ITranslationReaderProvider provider = factory.Create(rootPath);
            ITranslationReader reader;
            Assert.AreEqual(true, provider.TryGetReader(new CultureInfo("en-US"), Assembly.GetExecutingAssembly(), out reader));

            string translatedText;
            Assert.AreEqual(true, reader.TryGet("Hello, World!", out translatedText));
            Assert.AreEqual("Hello, World!", translatedText);


            /// CS ------------------
            Assert.AreEqual(true, provider.TryGetReader(new CultureInfo("cs-CZ"), Assembly.GetExecutingAssembly(), out reader));
            Assert.AreEqual(true, reader.TryGet("Hello, World!", out translatedText));
            Assert.AreEqual("Ahoj!", translatedText);
        }

        [TestMethod]
        public void GetText()
        {
            TranslationReaderCollection collection = new TranslationReaderCollection()
                .Add(new CultureInfo("cs-CZ"), new DefaultTranslationReader().Add("Hello, World!", "Ahoj všichni!"));

            ITranslationReaderProvider provider = new GetTextTranslationReaderProvider(
                new EnumerationCultureProvider(new CultureInfo("en-US").ParentsWithSelf()), 
                collection
            );


            /// EN ------------------
            ITranslationReader reader;
            Assert.AreEqual(true, provider.TryGetReader(new CultureInfo("en-US"), Assembly.GetExecutingAssembly(), out reader));
            string translatedText;
            Assert.AreEqual(true, reader.TryGet("Hello, World!", out translatedText));
            Assert.AreEqual("Hello, World!", translatedText);

            Assert.AreEqual(true, reader.TryGet("Hello!", out translatedText));
            Assert.AreEqual("Hello!", translatedText);


            /// CS ------------------
            Assert.AreEqual(true, provider.TryGetReader(new CultureInfo("cs-CZ"), Assembly.GetExecutingAssembly(), out reader));
            Assert.AreEqual(true, reader.TryGet("Hello, World!", out translatedText));
            Assert.AreEqual("Ahoj všichni!", translatedText);

            Assert.AreEqual(false, reader.TryGet("Hello!", out translatedText));
            Assert.AreEqual(null, translatedText);
        }
    }
}
