using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.FileResources
{
    public class FileStylesheet : WithMetaBase, IStylesheet
    {
        public string Source { get; private set; }

        public FileStylesheet(string source)
            : this(source, new Dictionary<string, string>())
        { }

        public FileStylesheet(string source, IDictionary<string, string> metadata)
            : base(metadata)
        {
            Guard.NotNullOrEmpty(source, "source");
            Source = source;
        }
    }
}
