using Neptuo;
using Neptuo.Models.Features;
using Neptuo.FileSystems;
using Neptuo.FileSystems.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.FileSystems
{
    class TestLocal
    {
        public static void Test()
        {
            //Directory.EnumerateFiles(null, null, SearchOption.AllDirectories);
            //Directory.EnumerateDirectories()
            //Path.GetExtension()
            //FileAttributes

            IFile file = null;
            Ensure.Condition.HasFeature<IFileContentSize>(file);
        }
    }
}
