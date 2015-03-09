using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Result from compiling assemblies using <see cref="IDynamicCompiler"/>.
    /// </summary>
    public interface ICompilerResult
    {
        /// <summary>
        /// If <c>true</c>, compilation was successfull; otherwise <c>false</c>.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Enumeration of compilation errors.
        /// May also contain warnings (for success <see cref="ICompilerResult.IsSuccess"/>).
        /// </summary>
        IEnumerable<IErrorInfo> Errors { get; }
    }
}
