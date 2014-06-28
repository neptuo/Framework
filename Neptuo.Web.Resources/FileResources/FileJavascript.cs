using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.FileResources
{
    public class FileJavascript : IJavascript
    {
        public string Source { get; private set; }

        public FileJavascript(string source)
        {
            Guard.NotNullOrEmpty(source, "source");
            Source = source;
        }
    }
}
