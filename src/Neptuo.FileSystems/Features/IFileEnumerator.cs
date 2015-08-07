using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Provides enumeration of files.
    /// </summary>
    public interface IFileEnumerator : IEnumerable<IFile>
    { }
}
