using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.FileSystems;
using Neptuo.FileSystems.Features;
using Neptuo.FileSystems.Features.Searching;
using Neptuo.Models.Features;
using System;
using System.Collections.Generic;
using System.IO;

namespace UnitTest.FileSystems
{
    public class T_FileSystems
    {
        [TestClass]
        public class Local
        {
            [TestMethod]
            public void CompleteTest()
            {
                string rootPath = @"C:\Temp\FileSystems";
                Assert.AreEqual(true, Directory.Exists(rootPath));
                CleanUpFileSystem(rootPath);

                IFileSystem fileSystem = new LocalFileSystem(rootPath);
                IFileSystemConstant constants = fileSystem.With<IFileSystemConstant>();
                IDirectory rootDirectory = fileSystem.RootDirectory;

                Assert.AreEqual('\\', constants.DirectorySeparatorChar);

                // Create directory
                IDirectory t1 = rootDirectory.With<IDirectoryCreator>().Create("T1");
                Assert.AreEqual("T1", t1.Name);
                Assert.AreEqual(Path.Combine(rootPath, "T1"), t1.With<IAbsolutePath>().AbsolutePath);
                EnsureAncestors(t1.With<IAncestorEnumerator>(), "FileSystems", "Temp", "C:\\");

                // Rename directory.
                t1.With<IDirectoryRenamer>().ChangeName("T1.1");
                Assert.AreEqual("T1.1", t1.Name);
                Assert.AreEqual(Path.Combine(rootPath, "T1.1"), t1.With<IAbsolutePath>().AbsolutePath);

                //Delete directory
                t1.With<IDirectoryDeleter>().Delete();
                Assert.AreEqual(false, Directory.Exists(Path.Combine(rootPath, "T1")));
                



                IFileNameSearch fileNameSearch = new LocalSearchProvider(@"C:\Temp");
                IEnumerable<IFile> files = fileNameSearch.FindFiles(TextSearch.CreateSuffixed("M"), TextSearch.CreateSuffixed("t"));

                foreach (IFile file in files)
                    Console.WriteLine(file);
                    //Console.WriteLine(file.With<IAbsolutePath>().AbsolutePath); 
            }

            private void CleanUpFileSystem(string rootPath)
            {
                foreach (string directory in Directory.GetDirectories(rootPath))
                    Directory.Delete(directory, true);

                foreach (string file in Directory.GetFiles(rootPath))
                    File.Delete(file);
            }

            private void EnsureAncestors(IAncestorEnumerator ancestors, params string[] names)
            {
                int index = 0;
                foreach (IDirectory ancestor in ancestors)
                {
                    Assert.AreEqual(names[index], ancestor.Name);
                    index++;
                }
            }
        }
    }
}
