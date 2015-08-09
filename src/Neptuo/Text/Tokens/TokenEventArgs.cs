using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Text.Tokens
{
    /// <summary>
    /// When parser finds token, this object describes newly found token, original text value and token position in that text.
    /// </summary>
    public class TokenEventArgs : EventArgs
    {
        /// <summary>
        /// Original text value passed to parser.
        /// </summary>
        public string OriginalContent { get; private set; }

        /// <summary>
        /// Parsed token.
        /// </summary>
        public Token Token { get; private set; }

        /// <summary>
        /// Token first character index in <see cref="TokenEventArgs.OriginalContent"/>.
        /// </summary>
        public int StartPosition { get; private set; }

        /// <summary>
        /// Token last characted index in <see cref="TokenEventArgs.OriginalContent"/>.
        /// </summary>
        public int EndPosition { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="originalContent">Original text value passed to parser.</param>
        /// <param name="token">Parsed token.</param>
        /// <param name="startPosition">Token first character index in <paramref name="originalContent"/>.</param>
        /// <param name="endPosition">Token last characted index in <paramref name="originalContent"/>.</param>
        public TokenEventArgs(string originalContent, Token token, int startPosition, int endPosition)
        {
            Ensure.NotNullOrEmpty(originalContent, "originalContent");
            Ensure.NotNull(token, "token");
            Ensure.PositiveOrZero(startPosition, "startPosition");
            Ensure.Positive(endPosition, "endPosition");

            if (endPosition <= startPosition)
                Ensure.Exception.ArgumentOutOfRange("endPosition", "End position index must be greater that start position index");

            OriginalContent = originalContent;
            Token = token;
            StartPosition = startPosition;
            EndPosition = endPosition;
        }
    }
}
