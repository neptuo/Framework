using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Tokens
{
    public class TokenParserConfiguration
    {
        public bool AllowAttributes { get; set; }
        public bool AllowEscapeSequence { get; set; }
        public bool AllowDefaultAttribute { get; set; }
        public bool AllowMultipleTokens { get; set; }
        public bool AllowTextContent { get; set; }
    }
}
