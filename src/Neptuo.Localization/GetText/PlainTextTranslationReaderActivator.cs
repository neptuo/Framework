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
    /// Creates <see cref="ITranslationReader"/> from files structured as Key=Value
    /// </summary>
    public class PlainTextTranslationReaderActivator : IActivator<ITranslationReader, Stream>, IActivator<ITranslationReader, string>
    {
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

        public ITranslationReader Create(Stream fileContent)
        {
            DefaultTranslationReader result = new DefaultTranslationReader();
            using (StreamReader reader = new StreamReader(fileContent))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int splitIndex = FindSplitPosition(line);
                    if (splitIndex >= 0)
                    {
                        string originalText = line.Substring(0, splitIndex);
                        string translatedText = line.Substring(splitIndex + 1);
                        result.Add(originalText, translatedText);
                    }
                }
            }

            return result;
        }

        private int FindSplitPosition(string line)
        {
            int indexOfEqual = -1;
            bool isFinished = false;
            do
            {
                indexOfEqual = line.IndexOf('=', indexOfEqual + 1);
                if (indexOfEqual >= 0)
                {
                    if (indexOfEqual > 0 && line[indexOfEqual - 1] == '\\')
                        continue;

                }
                isFinished = true;
            } while (!isFinished);

            return indexOfEqual;
        }
    }
}
