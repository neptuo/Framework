using Neptuo.FileSystems.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Enumerates directories on local file system path.
    /// </summary>
    public class LocalAncestorEnumerator : IEnumerable<IDirectory>, IAncestorEnumerator
    {
        private readonly string path;

        /// <summary>
        /// Creates new instance that enumerates parent directories on <paramref name="path"/>.
        /// </summary>
        /// <param name="path">Path to enumerate ancestors on.</param>
        public LocalAncestorEnumerator(string path)
        {
            Ensure.NotNull(path, "path");
            this.path = path;
        }

        public IEnumerator<IDirectory> GetEnumerator()
        {
            return new Enumerator(path);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Enumerator : EnumeratorBase<IDirectory>
        {
            private string originalPath;
            private string path;
            private IDirectory current;

            public Enumerator(string path)
            {
                this.originalPath = path;
                this.path = path;
            }

            public override IDirectory Current
            {
                get
                {
                    if (current == null)
                        current = new LocalDirectory(path);

                    return current;
                }
            }

            public override bool MoveNext()
            {
                path = Path.GetDirectoryName(path);
                current = null;
                return Directory.Exists(path);
            }

            public override void Reset()
            {
                path = originalPath;
            }
        }
    }
}
