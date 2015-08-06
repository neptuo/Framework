using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.FileSystems;
using Neptuo.FileSystems.Features;
using Neptuo.FileSystems.Features.Searching;
using Neptuo.Models.Features;
using System;
using System.Collections.Generic;

namespace UnitTest.FileSystems
{
    public class T_FileSystems
    {
        [TestClass]
        public class Local
        {
            [TestMethod]
            public void FileSearch()
            {
                IFileNameSearch fileNameSearch = new LocalSearchProvider(@"C:\Temp");
                IEnumerable<IFile> files = fileNameSearch.FindFiles(TextSearch.CreateSuffixed("M"), TextSearch.CreateSuffixed("t"));

                foreach (IFile file in files)
                    Console.WriteLine(file);
                    //Console.WriteLine(file.With<IAbsolutePath>().AbsolutePath); 
            }
        }
    }
}
