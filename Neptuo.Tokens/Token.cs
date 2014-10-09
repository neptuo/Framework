using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Tokens
{
    /// <summary>
    /// Describes parsed token. Name of token is splitted into <see cref="Token.Prefix"/> and <see cref="Token.Name"/>, <see cref="Token.Fullname"/> joins both.
    /// Default attributes are in <see cref="Token.DefaultAttributes"/> collection.
    /// Named attributes are in <see cref="Token.Attributes"/> collection.
    /// </summary>
    public class Token
    {
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
        /// Collection of named attributes.
        /// </summary>
        public List<TokenAttribute> Attributes { get; private set; }

        /// <summary>
        /// Collection of default (not named) attributes.
        /// </summary>
        public List<string> DefaultAttributes { get; private set; }

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public Token()
        {
            Attributes = new List<TokenAttribute>();
            DefaultAttributes = new List<string>();
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
