using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.DataAccess.EntityFramework
{
    public class UnitOfWorkException : Exception
    {
        public UnitOfWorkException(string message)
            : base(message)
        { }
    }
}
