using Neptuo.Activators;
using Neptuo.Globalization;
using Neptuo.Localization.Readers.Providers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization.Readers.Factories
{
    /// <summary>
    /// Creates translation reader providers from file system directory using these conventions:
    /// - Only passed directory is searched; sub-directories are not searched.
    /// - All files matching search pattern (standard <see cref="Directory.EnumerateFiles"/> search pattern) are used.
    /// - Files containing only culture in its name, are used as global; eg.: cs-CZ.txt, en-US.txt.
    /// - Files, which have culture prefixed by some name and dot, are used as assembly specific; eg.: App.Backend.cs-CZ.txt, App.Frontend.en-US.txt.
    /// </summary>
    public class DirectoryTranslationReaderProviderFactory : IFactory<ITranslationReaderProvider, string>
    {
        private readonly IFactory<ITranslationReader, Stream> readerFactory;
        private readonly string searchPattern;

        /// <summary>
        /// Creates new instance that searches for '*.txt' and uses <see cref="PlainTextTranslationReaderFactory"/>.
        /// </summary>
        public DirectoryTranslationReaderProviderFactory()
            : this(new PlainTextTranslationReaderFactory(), "*.txt")
        { }

        /// <summary>
        /// Creates new instance that creates readers by <paramref name="readerFactory"/>.
        /// </summary>
        /// <param name="readerFactory">Reader factory.</param>
        /// <param name="searchPattern">Standard <see cref="Directory.EnumerateFiles"/> search pattern.</param>
        public DirectoryTranslationReaderProviderFactory(IFactory<ITranslationReader, Stream> readerFactory, string searchPattern)
        {
            Ensure.NotNull(readerFactory, "readerFactory");
            Ensure.NotNullOrEmpty(searchPattern, "searchPattern");
            this.readerFactory = readerFactory;
            this.searchPattern = searchPattern;
        }

        /// <summary>
        /// Creates translation reader provider from directory path.
        /// </summary>
        /// <param name="directoryPath">Path to directory to load readers from.</param>
        /// <returns></returns>
        public ITranslationReaderProvider Create(string directoryPath)
        {
            TranslationReaderCollection result = new TranslationReaderCollection();
            foreach (string filePath in Directory.EnumerateFiles(directoryPath, searchPattern))
            {
                using(FileStream fileStream = new FileStream(filePath, FileMode.Open))
	            {
                    ITranslationReader reader = readerFactory.Create(fileStream);
                    TryAddReader(result, filePath, reader);
                }
            }

            return result;
        }

        private bool TryAddReader(TranslationReaderCollection collection, string filePath, ITranslationReader reader)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            CultureInfo culture;
            if (CultureInfoParser.TryParse(fileName, out culture))
            {
                collection.Add(culture, reader);
                return true;
            }

            // Everything before last '.' use as assembly name; everthing behind last '.' use as culture info.
            string assemblyName = Path.GetFileNameWithoutExtension(fileName);
            string cultureName = Path.GetExtension(fileName);

            if(CultureInfoParser.TryParse(cultureName, out culture))
            {
                collection.Add(culture, assemblyName, reader);
                return true;
            }

            return false;
        }
    }
}
