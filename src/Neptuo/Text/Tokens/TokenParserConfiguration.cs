using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Text.Tokens
{
    /// <summary>
    /// Feature configuration of <see cref="TokenParser"/>.
    /// </summary>
    public class TokenParserConfiguration
    {
        /// <summary>
        /// Whether content can contain text character before, between or after tokens.
        /// </summary>
        public bool AllowTextContent { get; set; }

        /// <summary>
        /// Whether escape sequences in format '{{' text '}}' are allowed.
        /// </summary>
        public bool AllowEscapeSequence { get; set; }

        /// <summary>
        /// Whether content can contain multiple tokens.
        /// </summary>
        public bool AllowMultipleTokens { get; set; }

        /// <summary>
        /// Whether default attributes are allowed.
        /// </summary>
        public bool AllowDefaultAttributes { get; set; }

        /// <summary>
        /// Whether named attributes are allowed.
        /// </summary>
        public bool AllowAttributes { get; set; }
    }
}
