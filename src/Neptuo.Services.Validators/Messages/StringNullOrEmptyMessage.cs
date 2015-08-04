using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Validators.Messages
{
    /// <summary>
    /// Validation message for string not 'null or empty'.
    /// </summary>
    public class StringNullOrEmptyMessage : ValidationMessageBase
    {
        /// <summary>
        /// Creates new instance for <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Validation message key.</param>
        public StringNullOrEmptyMessage(string key)
            : base(key)
        { }

        public override string ToString()
        {
            return String.Format("{0} can't be empty.", Key);
        }
    }
}
