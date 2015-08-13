using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization.Readers.Providers
{
    /// <summary>
    /// In-memory (collection) implementation of <see cref="ITranslationReaderProvider"/>.
    /// </summary>
    public class TranslationReaderCollection : ITranslationReaderProvider
    {
        private readonly Dictionary<CultureInfo, Dictionary<string, List<ITranslationReader>>> storage = new Dictionary<CultureInfo, Dictionary<string, List<ITranslationReader>>>();

        /// <summary>
        /// Adds <paramref name="reader"/> for <paramref name="culture"/> and <paramref name="assemblyName"/>.
        /// </summary>
        /// <param name="culture">Culture to add <paramref name="reader"/> for.</param>
        /// <param name="assemblyName">Name of the assembly to add <paramref name="reader"/> for.</param>
        /// <param name="reader">Translation reader.</param>
        /// <returns>Self (for fluency).</returns>
        public TranslationReaderCollection Add(CultureInfo culture, string assemblyName, ITranslationReader reader)
        {
            Ensure.NotNull(culture, "culture");
            Ensure.NotNull(reader, "reader");

            Dictionary<string, List<ITranslationReader>> cultureReaders;
            if (!storage.TryGetValue(culture, out cultureReaders))
                storage[culture] = cultureReaders = new Dictionary<string, List<ITranslationReader>>();

            List<ITranslationReader> assemblyReaders;
            if (!cultureReaders.TryGetValue(assemblyName, out assemblyReaders))
                cultureReaders[assemblyName] = assemblyReaders = new List<ITranslationReader>();

            assemblyReaders.Add(reader);
            return this;
        }

        /// <summary>
        /// Adds <paramref name="reader"/> for <paramref name="culture"/>.
        /// </summary>
        /// <param name="culture">Culture to add <paramref name="reader"/> for.</param>
        /// <param name="reader">Translation reader.</param>
        /// <returns>Self (for fluency).</returns>
        public TranslationReaderCollection Add(CultureInfo culture, ITranslationReader reader)
        {
            Add(culture, String.Empty, reader);
            return this;
        }

        /// <summary>
        /// Adds <paramref name="reader"/> for <paramref name="culture"/> and <paramref name="assembly"/>.
        /// </summary>
        /// <param name="culture">Culture to add <paramref name="reader"/> for.</param>
        /// <param name="assembly">Assembly to add <paramref name="reader"/> for.</param>
        /// <param name="reader">Translation reader.</param>
        /// <returns>Self (for fluency).</returns>
        public TranslationReaderCollection Add(CultureInfo culture, Assembly assembly, ITranslationReader reader)
        {
            Ensure.NotNull(culture, "culture");
            Ensure.NotNull(assembly, "assembly");
            string assemblyName = GetAssemblyName(assembly);
            Add(culture, assemblyName, reader);
            return this;
        }

        public bool TryGetReader(CultureInfo culture, Assembly assembly, out ITranslationReader reader)
        {
            Ensure.NotNull(culture, "culture");
            Ensure.NotNull(assembly, "assembly");
            string assemblyName = GetAssemblyName(assembly);

            IEnumerable<ITranslationReader> assemblyReaders = FindReaders(culture, assemblyName);
            IEnumerable<ITranslationReader> globalReaders = FindReaders(culture, String.Empty);
            if (assemblyReaders != null || globalReaders != null)
            {
                if (assemblyReaders == null)
                    reader = new EnumerationTranslationReader(globalReaders);
                else if (globalReaders == null)
                    reader = new EnumerationTranslationReader(assemblyReaders);
                else
                    reader = new EnumerationTranslationReader(Enumerable.Concat(assemblyReaders, globalReaders));

                return true;
            }

            reader = null;
            return false;
        }

        private IEnumerable<ITranslationReader> FindReaders(CultureInfo culture, string assemblyName)
        {
            Dictionary<string, List<ITranslationReader>> cultureReaders;
            if (storage.TryGetValue(culture, out cultureReaders))
            {
                List<ITranslationReader> assemblyReaders;
                if (cultureReaders.TryGetValue(assemblyName, out assemblyReaders))
                    return assemblyReaders;
            }

            return null;
        }

        private string GetAssemblyName(Assembly assembly)
        {
            string assemblyName = assembly.FullName;
            int indexOfComma = assemblyName.IndexOf(',');
            if (indexOfComma >= 0)
                return assemblyName.Substring(0, indexOfComma);

            return assemblyName;
        }
    }
}
