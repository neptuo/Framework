using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization.GetText
{
    /// <summary>
    /// Creates <see cref="ITranslationReader"/> from files structured as:
    /// first non-empty lines: Message key (original text),
    /// second non-empty lines: Translation (translated text),
    /// two empty lines: Message-translation separation.
    /// </summary>
    /// <example>
    /// Name
    /// 
    /// Jméno
    /// 
    /// 
    /// Surname
    /// 
    /// Příjmení
    /// 
    /// 
    /// </example>
    public class PlainTextTranslationReaderActivator : IActivator<ITranslationReader, Stream>, IActivator<ITranslationReader, string>
    {
        public ITranslationReader Create(Stream fileContent)
        {
            using (StreamReader reader = new StreamReader(fileContent))
            {
                string line = reader.ReadLine();
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Creates translation reader from file path.
        /// </summary>
        /// <param name="fileName">Path to file.</param>
        /// <returns>Translation reader reading from <paramref name="fileName"/>.</returns>
        public ITranslationReader Create(string fileName)
        {
            using (Stream fileContent = new FileStream(fileName, FileMode.Open))
            {
                return Create(fileContent);
            }
        }
    }
}
