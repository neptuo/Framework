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
        bool IsSuccess { get; }

        IEnumerable<IErrorInfo> Errors { get; }
    }
}
