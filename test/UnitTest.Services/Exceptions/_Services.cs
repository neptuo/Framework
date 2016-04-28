using Neptuo.Exceptions.Handlers;
using Neptuo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions
{
    public class AggregateRootExceptionHandler : IExceptionHandler<AggregateRootException>
    {
        public void Handle(AggregateRootException exception)
        {
            Console.WriteLine(exception);
        }
    }

    public class ArgumentExceptionHandler : IExceptionHandler<ArgumentException>
    {
        public void Handle(ArgumentException exception)
        {
            Console.WriteLine(exception);
        }
    }

}
