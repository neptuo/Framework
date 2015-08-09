using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Text.Tokens
{
    /// <summary>
    /// Describes named token attribute.
    /// </summary>
    public class TokenAttribute
    {
        /// <summary>
        /// The owner of this attribute.
        /// </summary>
        public Token OwnerToken { get; internal set; }

        /// <summary>
        /// Attribute name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Attribute value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Creates new instance with name <paramref name="name"/> and optional value <paramref name="value"/>.
        /// </summary>
        /// <param name="name">Attribute name.</param>
        /// <param name="value">Attribute value.</param>
        public TokenAttribute(string name, string value = null)
        {
            Ensure.NotNullOrEmpty(name, "name");
            Name = name;
            Value = value;
        }
    }
}
