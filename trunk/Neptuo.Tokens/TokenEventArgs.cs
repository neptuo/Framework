using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Tokens
{
    public class TokenEventArgs : EventArgs
    {
        public string OriginalContent { get; private set; }
        public Token Token { get; private set; }
        public int StartPosition { get; private set; }
        public int EndPosition { get; private set; }

        public TokenEventArgs(string originalContent, Token token, int startPosition, int endPosition)
        {
            OriginalContent = originalContent;
            Token = token;
            StartPosition = startPosition;
            EndPosition = endPosition;
        }
    }

    public delegate void OnParsedToken(TokenEventArgs e);
}
