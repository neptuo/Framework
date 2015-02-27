using Neptuo.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    public class LocalFileSystemKey : KeyBase
    {
        public static LocalFileSystemKey Create(string fullPath, string type)
        {
            Guard.NotNullOrEmpty(fullPath, "fullPath");
            Guard.NotNullOrEmpty(type, "type");
            
            if (!Path.IsPathRooted(fullPath))
                throw Guard.Exception.Argument("fullPath", "Path must be rooted.");

            if (!Directory.Exists(fullPath) && !File.Exists(fullPath))
                throw Guard.Exception.Argument("fullPath", "Provided path must be existing directory.");

            return new LocalFileSystemKey(fullPath, type);
        }

        public static LocalFileSystemKey Empty(string type)
        {
            Guard.NotNullOrEmpty(type, "type");
            return new LocalFileSystemKey(type);
        }

        public string FullPath { get; private set; }

        protected LocalFileSystemKey(string type)
            : base(type, true)
        { }

        protected LocalFileSystemKey(string fullPath, string type)
            : base(type, false)
        {
            FullPath = fullPath;
        }

        protected override bool Equals(KeyBase other)
        {
            LocalFileSystemKey key;
            if (Converts.Try<IKey, LocalFileSystemKey>(other, out key))
                return false;

            return FullPath == key.FullPath;
        }

        protected override int CompareValueTo(KeyBase other)
        {
            LocalFileSystemKey key;
            if (Converts.Try<IKey, LocalFileSystemKey>(other, out key))
                return 1;

            return FullPath.CompareTo(key.FullPath);
        }

        protected override int GetValueHashCode()
        {
            return FullPath.GetHashCode();
        }
    }
}
