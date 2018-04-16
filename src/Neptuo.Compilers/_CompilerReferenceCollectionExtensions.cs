using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Common extensions for <see cref="CompilerReferenceCollection"/>.
    /// </summary>
    public static class _CompilerReferenceCollectionExtensions
    {
        /// <summary>
        /// Adds enumeration of bin directories to reference collection.
        /// </summary>
        /// <param name="collection">Target collection of references.</param>
        /// <param name="binDirectories">Enumeration of directories to add.</param>
        /// <returns><paramref name="collection"/> (for fluency).</returns>
        public static CompilerReferenceCollection AddDirectories(this CompilerReferenceCollection collection, IEnumerable<string> binDirectories)
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(binDirectories, "binDirectories");

            foreach (string binDirectory in binDirectories)
                collection.AddDirectory(binDirectory);

            return collection;
        }
    }
}
