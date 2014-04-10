using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.SharpKit.CodeGenerator
{
    public class SharpKitCompilerException : Exception
    {
        public SharpKitCompiler.ExecuteResult ExecuteResult { get; set; }

        public SharpKitCompilerException(SharpKitCompiler.ExecuteResult executeResult)
        {
            ExecuteResult = executeResult;
        }
    }
}
