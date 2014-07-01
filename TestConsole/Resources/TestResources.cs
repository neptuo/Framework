using Neptuo.FileSystems;
using Neptuo.Web.Resources;
using Neptuo.Web.Resources.Collections;
using Neptuo.Web.Resources.Providers;
using Neptuo.Web.Resources.Providers.XmlProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Resources
{
    class TestResources
    {
        public static void Test()
        {
            IReadOnlyFile file = StaticFileSystem.FromFilePath(Path.Combine(Environment.CurrentDirectory, "Resources.xml"));
            using (IResourceCollectionInitializer loader = new XmlResourceReader(file))
            {
                loader.FillCollection(ResourceTable.Resources);
            }

            IResourceCollection collection = ResourceTable.Resources;

            IResource resource;
            if (!collection.TryGet("FluentConsole", out resource))
                throw new Exception();

            WriteResource(resource);
        }

        private static void WriteResource(IResource resource)
        {
            Console.WriteLine(resource.Name);

            foreach (IJavascript javascript in resource.EnumerateJavascripts())
                Console.WriteLine(javascript.Source);

            foreach (IStylesheet stylesheet in resource.EnumerateStylesheets())
                Console.WriteLine(stylesheet.Source);

            foreach (IResource dependency in resource.EnumerateDependencies())
                WriteResource(dependency);
        }
    }
}
