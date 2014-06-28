using Neptuo.FileSystems;
using Neptuo.Web.Resources;
using Neptuo.Web.Resources.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Resources
{
    class TestResources
    {
        public static void Test()
        {
            ResourceTable.SetXmlCollection(new File(System.IO.Path.Combine(Environment.CurrentDirectory, "Resources.xml")));
            IResourceCollection collection = ResourceTable.Resources;

            IResource resource;
            if (!collection.TryGet("FluentConsole", out resource))
                throw new Exception();

            Console.WriteLine(resource.Name);

            foreach (IJavascript javascript in resource.EnumerateJavascripts())
                Console.WriteLine(javascript.Source);

            foreach (IStylesheet stylesheet in resource.EnumerateStylesheets())
                Console.WriteLine(stylesheet.Source);
        }
    }
}
