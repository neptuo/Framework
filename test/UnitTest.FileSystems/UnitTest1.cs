using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.FileSystems;
using Neptuo.FileSystems.Features.Searching;
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
                IEnumerable<IFile> files = fileNameSearch.FindFiles(TextSearch.CreateSuffixed("N"), TextSearch.CreateSuffixed("t"));
            }
        }
    }
}
