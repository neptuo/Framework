using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Text.Tokens
{
    /// <summary>
    /// String formatter using tokens.
    /// </summary>
    public class TokenWriter
    {
        private readonly List<TokenWriterItem> items = new List<TokenWriterItem>();

        /// <summary>
        /// Creates new instance for format string as <paramref name="format"/> without support for attributes on tokens.
        /// </summary>
        /// <param name="format">Format string.</param>
        public TokenWriter(string format)
            : this(format, false)
        {
        }

        /// <summary>
        /// Creates new instance for format string as <paramref name="format"/>.
        /// </summary>
        /// <param name="format">Format string.</param>
        /// <param name="isAttributeSupported">If <c>true</c> attributes on tokens are supported.</param>
        public TokenWriter(string format, bool isAttributeSupported)
        {
            Ensure.NotNullOrEmpty(format, "format");
            TokenFormatHelper.Parse(format, items, isAttributeSupported);
        }

        /// <summary>
        /// For each found token, calls <paramref name="tokenMapper"/> with token name and replaces that token with returned value.
        /// </summary>
        /// <param name="tokenMapper">Token name to value replacer.</param>
        /// <returns>Formatted string with replaced tokens.</returns>
        public string Format(Func<string, string> tokenMapper)
        {
            StringBuilder result = new StringBuilder();
            foreach (TokenWriterItem item in items)
            {
                if (item.IsToken)
                    result.Append(tokenMapper(item.Token.Fullname));
                else
                    result.Append(item.Text);
            }

            return result.ToString();
        }

        /// <summary>
        /// For each found token, calls <paramref name="tokenMapper"/> with token name and replaces that token with returned value.
        /// </summary>
        /// <param name="tokenMapper">Token to value replacer.</param>
        /// <returns>Formatted string with replaced tokens.</returns>
        public string Format(Func<Token, string> tokenMapper)
        {
            StringBuilder result = new StringBuilder();
            foreach (TokenWriterItem item in items)
            {
                if (item.IsToken)
                    result.Append(tokenMapper(item.Token));
                else
                    result.Append(item.Text);
            }

            return result.ToString();
        }

        /// <summary>
        /// For each found token, tries to read string value from <paramref name="tokenMapper"/> and replaces that token with returned value.
        /// </summary>
        /// <param name="tokenMapper">Token name to token value replacer.</param>
        /// <returns>Formatted string with replaced tokens.</returns>
        public string Format(IReadOnlyKeyValueCollection tokenMapper)
        {
            StringBuilder result = new StringBuilder();
            foreach (TokenWriterItem item in items)
            {
                if (item.IsToken)
                    result.Append(tokenMapper.Get(item.Token.Fullname, ""));
                else
                    result.Append(item.Text);
            }

            return result.ToString();
        }
    }

    /// <summary>
    /// Helper class for initial format parsing process.
    /// </summary>
    internal class TokenFormatHelper
    {
        public static void Parse(string format, List<TokenWriterItem> items, bool isAttributeSupported)
        {
            int lastTokenEndIndex = 0;

            TokenParser parser = new TokenParser();
            parser.Configuration.AllowTextContent = true;
            parser.Configuration.AllowEscapeSequence = true;
            parser.Configuration.AllowMultipleTokens = true;
            parser.Configuration.AllowAttributes = isAttributeSupported;
            parser.OnParsedToken += (sender, e) =>
            {
                if (e.StartPosition > 0)
                {
                    int startIndex = items.Any() ? lastTokenEndIndex : 0;
                    if (startIndex < e.StartPosition)
                        items.Add(new TokenWriterItem(format.Substring(startIndex, e.StartPosition - startIndex)));
                }

                items.Add(new TokenWriterItem(e.Token));
                lastTokenEndIndex = e.EndPosition;
            };

            if (!parser.Parse(format))
                throw Ensure.Exception.ArgumentOutOfRange("format", "Format string '{0}' doesn't contain valid token format string.", format);

            if (lastTokenEndIndex < format.Length)
                items.Add(new TokenWriterItem(format.Substring(lastTokenEndIndex)));
        }
    }

    /// <summary>
    /// Represents part of format string.
    /// Is static text value or token.
    /// </summary>
    internal class TokenWriterItem
    {
        public string Text { get; private set; }
        public Token Token { get; private set; }

        public bool IsToken
        {
            get { return Token != null; }
        }

        public TokenWriterItem(string text)
        {
            Text = text;
        }

        public TokenWriterItem(Token token)
        {
            Token = token;
        }
    }
}