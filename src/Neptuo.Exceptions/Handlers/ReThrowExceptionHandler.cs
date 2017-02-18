using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    public class ReThrowExceptionHandler : IExceptionHandler, IExceptionHandler<Exception>
    {
        public void Handle(Exception exception)
        {
            ExceptionDispatchInfo info = ExceptionDispatchInfo.Capture(exception);
            info.Throw();
        }
    }
}
