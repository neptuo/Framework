using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Neptuo.SharpKit.CodeGenerator
{
    public class SharpKitCompilerContext
    {
        public TextReader Input { get; private set; }
        public TextWriter Output { get; private set; }

        public SharpKitCompilerContext(TextReader input, TextWriter output)
        {
            Input = input;
            Output = output;
        }
    }
}
