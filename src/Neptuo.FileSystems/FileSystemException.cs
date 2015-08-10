using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Generic exception that can occur in virtual file systems.
    /// </summary>
    public class FileSystemException :Exception
    {
        /// <summary>
        /// Create new instance with <paramref name="message"/> as context information.
        /// </summary>
        /// <param name="message">Context information.</param>
        public FileSystemException(string message)
            : base(message)
        { }

        /// <summary>
        /// Create new instance with <paramref name="message"/> as context information 
        /// and <paramref name="innerException"/> as inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">Context information.</param>
        /// <param name="innerException">Inner exception that is the cause of this exception.</param>
        public FileSystemException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected FileSystemException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
