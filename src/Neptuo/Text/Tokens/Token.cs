using Neptuo.Text.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Text.Tokens
{
    /// <summary>
    /// Describes parsed token. Name of token is splitted into <see cref="Token.Prefix"/> and <see cref="Token.Name"/>, <see cref="Token.Fullname"/> joins both.
    /// Default attributes are in <see cref="Token.DefaultAttributes"/> collection.
    /// Named attributes are in <see cref="Token.Attributes"/> collection.
    /// </summary>
    public class Token : IDocumentSpan
    {
        private readonly List<TokenAttribute> attributes;
        private readonly List<string> defaultAttributes;

        /// <summary>
        /// Token prefix. <see cref="Token.Fullname"/> part before ':'.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Token local name. <see cref="Token.Fullname"/> part after ':'.S
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Token full name. Joined <see cref="Token.Prefix"/> and <see cref="Token.Name"/>.
        /// </summary>
        public string Fullname
        {
            get
            {
                if (String.IsNullOrEmpty(Prefix))
                    return Name;

                return Prefix + ":" + Name;
            }
            set
            {
                int index = value.IndexOf(':');
                if (index != -1)
                {
                    Prefix = value.Substring(0, index);
                    Name = value.Substring(index + 1);
                }
                else
                {
                    Name = value;
                }
            }
        }

        /// <summary>
        /// Enumeration of named attributes.
        /// </summary>
        public IEnumerable<TokenAttribute> Attributes
        {
            get { return attributes; }
        }

        /// <summary>
        /// Enumeration of default (not named) attributes.
        /// </summary>
        public IEnumerable<string> DefaultAttributes
        {
            get { return defaultAttributes; }
        }

        public int LineIndex { get; private set; }
        public int EndLineIndex { get; private set; }
        public int ColumnIndex { get; private set; }
        public int EndColumnIndex { get; private set; }

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public Token()
        {
            attributes = new List<TokenAttribute>();
            defaultAttributes = new List<string>();
        }

        /// <summary>
        /// Updates source line info.
        /// </summary>
        /// <param name="lineNumber">Line number.</param>
        /// <param name="columnIndex">Index at line.</param>
        /// <param name="endLineNumber">Line number of range end.</param>
        /// <param name="endColumnIndex">Index at line of range end.</param>
        public void SetLineInfo(int lineNumber, int columnIndex, int endLineNumber, int endColumnIndex)
        {
            LineIndex = lineNumber;
            ColumnIndex = columnIndex;
            EndLineIndex = endLineNumber;
            EndColumnIndex = endColumnIndex;
        }

        /// <summary>
        /// Adds named attribute.
        /// </summary>
        /// <param name="attribute">New named attribute.</param>
        public void AddAttribute(TokenAttribute attribute)
        {
            Ensure.NotNull(attribute, "attribute");
            attribute.OwnerToken = this;
            attributes.Add(attribute);
        }

        /// <summary>
        /// Adds default (not named) attribute.
        /// </summary>
        /// <param name="defaultAttribute">Default (not named) attribute.</param>
        public void AddDefaultAttribute(string defaultAttribute)
        {
            defaultAttributes.Add(defaultAttribute);
        }

        /// <summary>
        /// Formats original token string.
        /// </summary>
        /// <returns>Original token string.</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("{" + Fullname);
            bool isFirstAttribute = true;
            foreach (string defaultAttribute in DefaultAttributes)
            {
                if (isFirstAttribute)
                {
                    isFirstAttribute = false;
                    result.Append(" ");
                }
                else
                {
                    result.Append(", ");
                }

                result.AppendFormat(defaultAttribute);
            }

            foreach (TokenAttribute attribute in Attributes)
            {
                if (isFirstAttribute)
                {
                    isFirstAttribute = false;
                    result.Append(" ");
                }
                else
                {
                    result.Append(", ");
                }

                result.AppendFormat("{0}={1}", attribute.Name, attribute.Value);
            }

            result.Append("}");
            return result.ToString();
        }
    }
}
