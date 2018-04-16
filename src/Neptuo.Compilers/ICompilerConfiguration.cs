using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Compiler configurable settings.
    /// </summary>
    public interface ICompilerConfiguration : IKeyValueCollection, ICloneable<ICompilerConfiguration>
    {
    }
}
