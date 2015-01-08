using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.SharpKit.Exugin.Exports
{
    public class NamespaceRegistry
    {
        private bool processWildcard;
        public string DefaultFilename { get; set; }
        public string FilenameFormat { get; set; }
        private Dictionary<string, string> mappings = new Dictionary<string, string>();

        public NamespaceRegistry(bool processWildcard, string defaultFilename, string filenameFormat)
        {
            this.processWildcard = processWildcard;
            this.DefaultFilename = defaultFilename;
            this.FilenameFormat = filenameFormat;
        }

        public void Add(string ns, string filename)
        {
            if (!processWildcard)
            {
                mappings[ns] = filename;
                return;
            }

            throw new NotImplementedException("Wildcards not yet supported");
        }

        public string GetFilename(string ns)
        {
            if (!processWildcard)
            {
                string filename = DefaultFilename;
                if (mappings.ContainsKey(ns))
                    filename = mappings[ns];

                return String.Format(FilenameFormat, filename);
            }

            throw new NotImplementedException("Wildcards not yet supported");
        }
    }
}
