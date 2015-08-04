using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Validators.Messages
{
    /// <summary>
    /// Validation message for comparing two properties.
    /// </summary>
    public class PropertyEqualMessage : ValidationMessageBase
    {
        /// <summary>
        /// Other key that is required to be equal to <see cref="PropertyEqualMessage.Key"/>.
        /// </summary>
        public string OtherKey { get; private set; }

        /// <summary>
        /// Creates new instance with required equality between <paramref name="key"/> and <paramref name="otherKey"/>.
        /// </summary>
        /// <param name="key">Target field key.</param>
        /// <param name="otherKey">Other key that is required to be equal to <see cref="PropertyEqualMessage.Key"/>.</param>
        public PropertyEqualMessage(string key, string otherKey)
            : base(key)
        {
            OtherKey = otherKey;
        }

        public override string ToString()
        {
            return String.Format("{0} must match {1}.", Key, OtherKey);
        }
    }
}
