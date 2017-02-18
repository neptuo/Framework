using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    /// <summary>
    /// An implementation of <see cref="IExceptionHandler"/> and <see cref="IExceptionHandler{T}"/> which simply rethrows the exception.
    /// It uses <see cref="ExceptionDispatchInfo"/> to restore captured exception state.
    /// </summary>
    public class ReThrowExceptionHandler : IExceptionHandler, IExceptionHandler<Exception>
    {
        public void Handle(Exception exception)
        {
            ExceptionDispatchInfo info = ExceptionDispatchInfo.Capture(exception);
            info.Throw();
        }
    }
}
