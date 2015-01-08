using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.FileResources
{
    public class FileJavascript : WithMetaBase, IJavascript, IWithMeta
    {
        public string Source { get; private set; }

        public FileJavascript(string source)
            : this(source, new Dictionary<string, string>())
        { }

        public FileJavascript(string source, IDictionary<string, string> metadata)
            : base(metadata)
        {
            Guard.NotNullOrEmpty(source, "source");
            Source = source;
        }
    }
}
