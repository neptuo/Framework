﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.FileSystems;
using Neptuo.FileSystems.Features;
using Neptuo.FileSystems.Features.Searching;
using Neptuo.Models.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                IFileSystemConstant constants = fileSystem.WithFileSystemConstant();
                IDirectory rootDirectory = fileSystem.RootDirectory;

                Assert.AreEqual('\\', constants.DirectorySeparatorChar);

                // Create directory
                IDirectory d1 = rootDirectory.WithDirectoryCreator().Create("D1");
                Assert.AreEqual("D1", d1.Name);
                Assert.AreEqual(Path.Combine(rootPath, "D1"), d1.WithAbsolutePath().AbsolutePath);
                EnsureAncestors(d1.WithAncestorEnumerator(), "FileSystems", "Temp", "C:\\");

                // Rename directory.
                d1.WithDirectoryRenamer().ChangeName("D1.1");
                Assert.AreEqual("D1.1", d1.Name);
                Assert.AreEqual(Path.Combine(rootPath, "D1.1"), d1.WithAbsolutePath().AbsolutePath);
                Assert.AreEqual(false, Directory.Exists(Path.Combine(rootPath, "D1")));
                Assert.AreEqual(true, Directory.Exists(Path.Combine(rootPath, "D1.1")));


                // Create file
                IFile f1 = d1.WithFileCreator().Create("F1", "txt");
                Assert.AreEqual(true, File.Exists(Path.Combine(rootPath, d1.Name, "F1.txt")));
                Assert.AreEqual(Path.Combine(d1.WithAbsolutePath().AbsolutePath, "F1.txt"), f1.WithAbsolutePath().AbsolutePath);
                Assert.AreEqual("F1", f1.Name);
                Assert.AreEqual("txt", f1.Extension);
                Assert.AreEqual(0, f1.WithFileContentSize().FileSize);

                // Write to file
                f1.WithFileContentUpdater().SetContent("Text");
                Assert.AreEqual("Text", File.ReadAllText(f1.WithAbsolutePath().AbsolutePath));

                // Append to file
                f1.WithFileContentAppender().AppendContent(".T1");
                Assert.AreEqual("Text.T1", File.ReadAllText(f1.WithAbsolutePath().AbsolutePath));

                // Override file content
                f1.WithFileContentUpdater().SetContent("T2");
                Assert.AreEqual("T2", File.ReadAllText(f1.WithAbsolutePath().AbsolutePath));

                // Read file content
                Assert.AreEqual("T2", f1.WithFileContentReader().GetContent());
                Assert.AreEqual(2, f1.WithFileContentSize().FileSize);

                // Rename file
                f1.WithFileRenamer().ChangeName("F1.1");
                Assert.AreEqual("F1.1", f1.Name);
                Assert.AreEqual("txt", f1.Extension);
                Assert.AreEqual(Path.Combine(d1.WithAbsolutePath().AbsolutePath, "F1.1.txt"), f1.WithAbsolutePath().AbsolutePath);
                Assert.AreEqual(false, File.Exists(Path.Combine(d1.WithAbsolutePath().AbsolutePath, "F1.txt")));
                Assert.AreEqual(true, File.Exists(Path.Combine(d1.WithAbsolutePath().AbsolutePath, "F1.1.txt")));

                // Enumerate files.
                IFileEnumerator fe1 = d1.WithFileEnumerator();
                Assert.AreEqual(1, fe1.Count());

                // Create files and directories.
                IFile f2 = d1.WithFileCreator().Create("F2", "txt");
                IFile f3 = d1.WithFileCreator().Create("F3", "rtf");
                IFile f4 = d1.WithFileCreator().Create("f4", "rtf");

                IDirectory d12 = d1.WithDirectoryCreator().Create("D2");
                IFile f121 = d12.WithFileCreator().Create("F1", "txt");
                IFile f122 = d12.WithFileCreator().Create("F2", "txt");
                IFile f123 = d12.WithFileCreator().Create("F3", "rtf");
                IFile f124 = d12.WithFileCreator().Create("f4", "rtf");

                // Enumerate directories.
                IDirectoryEnumerator de1 = d1.WithDirectoryEnumerator();
                Assert.AreEqual(1, de1.Count());

                // Searching directories
                IEnumerable<IDirectory> s1 = rootDirectory.WithDirectoryNameSearch().FindDirectories(TextSearch.CreatePrefixed("D"));
                Assert.AreEqual(1, s1.Count());

                IEnumerable<IDirectory> s2 = rootDirectory.WithDirectoryPathSearch().FindDirectories(TextSearch.CreatePrefixed("D"));
                Assert.AreEqual(2, s2.Count());

                IEnumerable<IDirectory> s3 = rootDirectory.WithDirectoryPathSearch().FindDirectories(TextSearch.CreateSuffixed("2"));
                Assert.AreEqual(1, s3.Count());

                IEnumerable<IDirectory> s4 = rootDirectory.WithDirectoryPathSearch().FindDirectories(TextSearch.CreateEmpty());
                Assert.AreEqual(2, s4.Count());

                // Searching files
                IEnumerable<IFile> s5 = rootDirectory.WithFileNameSearch().FindFiles(TextSearch.CreatePrefixed("f1"), TextSearch.CreateEmpty());
                Assert.AreEqual(0, s5.Count());

                IEnumerable<IFile> s6 = d1.WithFileNameSearch().FindFiles(TextSearch.CreatePrefixed("f1"), TextSearch.CreateEmpty());
                Assert.AreEqual(1, s6.Count());

                IEnumerable<IFile> s7 = d1.WithFileNameSearch().FindFiles(TextSearch.CreateEmpty(), TextSearch.CreateMatched("txt"));
                Assert.AreEqual(2, s7.Count());

                IEnumerable<IFile> s8 = d1.WithFileNameSearch().FindFiles(TextSearch.CreateContained("f"), TextSearch.CreateEmpty());
                Assert.AreEqual(4, s8.Count());

                IEnumerable<IFile> s9 = rootDirectory.WithFilePathSearch().FindFiles(TextSearch.CreateSuffixed("2"), TextSearch.CreateEmpty());
                Assert.AreEqual(2, s9.Count());


                // Delete file
                f1.WithFileDeleter().Delete();

                //Delete directory
                d1.WithDirectoryDeleter().Delete();
                Assert.AreEqual(false, Directory.Exists(Path.Combine(rootPath, "T1")));
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
