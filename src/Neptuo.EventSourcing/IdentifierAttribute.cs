using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// Defines unique type identifier.
    /// This attribute is determined by the persistent dispatchers to override handler identifier 
    /// which is by default build from assembly qualified type name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class IdentifierAttribute : Attribute
    {
        /// <summary>
        /// The value of the identifier.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Creates new instance with identifier as <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value of the identifier.</param>
        public IdentifierAttribute(string value)
        {
            Ensure.NotNullOrEmpty(value, "value");
            Value = value;
        }
    }
}
