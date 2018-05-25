using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// An exception raised from <see cref="KeyToParametersConverter.Get"/> methods.
    /// </summary>
    [Serializable]
    public class MissingKeyInParametersException : Exception
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public MissingKeyInParametersException()
        { }
    }
}
