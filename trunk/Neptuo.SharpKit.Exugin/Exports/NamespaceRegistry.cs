using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.SharpKit.Exugin.Exports
{
    /// <summary>
    /// Registr kam se který namespace má exportovat.
    /// </summary>
    public class NamespaceRegistry
    {
        private bool processWildcard;
        private string defaultFilename;
        private string filenameFormat;
        private Dictionary<string, string> mappings = new Dictionary<string, string>();

        /// <summary>
        /// Vytvoří registr.
        /// </summary>
        /// <param name="processWildcard">Zda se mají akceptovat wild cardy (*) v definici namespaců.</param>
        /// <param name="defaultFilename">Výchozí jméno souboru (pro neregistrované namespacy) na které se aplikuje <paramref name="filenameFormat"/>./param>
        /// <param name="filenameFormat">Formátovací řetězec, který se aplikuje na jméno souboru.</param>
        public NamespaceRegistry(bool processWildcard, string defaultFilename, string filenameFormat)
        {
            this.processWildcard = processWildcard;
            this.defaultFilename = defaultFilename;
            this.filenameFormat = filenameFormat;
        }

        public void SetDefaults(string defaultFilename, string filenameFormat)
        {
            this.defaultFilename = defaultFilename;
            this.filenameFormat = filenameFormat;
        }

        /// <summary>
        /// Přidá do registru namespace.
        /// </summary>
        /// <param name="ns">Jméno namespacu.</param>
        /// <param name="filename">Jméno souboru, kam se má <paramref name="ns"/> exportovat (aplikuje se na něj <see cref="filenameFormat"/>).</param>
        public void Add(string ns, string filename)
        {
            if (!processWildcard)
            {
                mappings[ns] = filename;
                return;
            }

            throw new NotImplementedException("Wildcards not yet supported");
        }

        /// <summary>
        /// Vrací jméno souboru kam se má <paramref name="ns"/> exportovat.
        /// Již na něj aplikuje <see cref="filenameFormat"/>.
        /// </summary>
        /// <param name="ns">Jméno namespacu.</param>
        /// <returns>Jméno souboru kam se má <paramref name="ns"/> exportovat.</returns>
        public string GetFilename(string ns)
        {
            if (!processWildcard)
            {
                string filename = defaultFilename;
                if (mappings.ContainsKey(ns))
                    filename = mappings[ns];

                return String.Format(filenameFormat, filename);
            }

            throw new NotImplementedException("Wildcards not yet supported");
        }
    }
}
