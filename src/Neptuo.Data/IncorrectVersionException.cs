using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    public class IncorrectVersionException : DataException
    {
        public IncorrectVersionException(Key key)
            : base(String.Format("Incorrect version for '{0}'", key))
        { }

        public IncorrectVersionException(string message)
            : base(message)
        { }

        public IncorrectVersionException(Exception innerException)
            : this(String.Empty, innerException)
        { }

        public IncorrectVersionException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
