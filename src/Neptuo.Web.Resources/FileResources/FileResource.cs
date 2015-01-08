using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.FileResources
{
    public class FileResource : WithMetaBase, IResource
    {
        protected List<IJavascript> Javascripts { get; private set; }
        protected List<IStylesheet> Stylesheets { get; private set; }
        protected List<IResource> Dependencies { get; private set; }

        public string Name { get; private set; }

        public FileResource(string name)
            : this(name, new Dictionary<string, string>())
        { }

        public FileResource(string name, IDictionary<string, string> metadata)
            : base(metadata)
        {
            Guard.NotNullOrEmpty(name, "name");
            Name = name;
            Javascripts = new List<IJavascript>();
            Stylesheets = new List<IStylesheet>();
            Dependencies = new List<IResource>();
        }

        public void AddJavascript(IJavascript javascript)
        {
            Guard.NotNull(javascript, "javascript");
            if (Javascripts.Contains(javascript))
                throw new ArgumentException(String.Format("Resource '{0}' already contains javascript '{1}'.", Name, javascript.Source), "javascript");

            Javascripts.Add(javascript);
        }

        public void AddStylesheet(IStylesheet Stylesheet)
        {
            Guard.NotNull(Stylesheet, "stylesheet");
            if (Stylesheets.Contains(Stylesheet))
                throw new ArgumentException(String.Format("Resource '{0}' already contains stylesheet '{1}'.", Name, Stylesheet.Source), "stylesheet");

            Stylesheets.Add(Stylesheet);
        }

        public void AddDependency(IResource dependency)
        {
            Guard.NotNull(dependency, "dependency");
            if (Dependencies.Contains(dependency))
                throw new ArgumentException(String.Format("Resource '{0}' already contains dependency on '{1}'.", Name, dependency.Name), "dependency");

            Dependencies.Add(dependency);
        }

        public IEnumerable<IJavascript> EnumerateJavascripts()
        {
            return Javascripts;
        }

        public IEnumerable<IStylesheet> EnumerateStylesheets()
        {
            return Stylesheets;
        }

        public IEnumerable<IResource> EnumerateDependencies()
        {
            return Dependencies;
        }
    }
}
