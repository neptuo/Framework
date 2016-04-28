using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    /// <summary>
    /// The builder of various filters for handling specific exceptions.
    /// </summary>
    public class ExceptionHandlerBuilder : ExceptionHandlerBuilder<Exception>
    {

    }

    public class ExceptionHandlerBuilder<T>
        where T : Exception
    {
        public ExceptionHandlerBuilder<TBase> Filter<TBase>()
            where TBase : T
        {
            throw new NotImplementedException();
        }

        public ExceptionHandlerBuilder<T> Filter(Func<T, bool> filter)
        {
            throw new NotImplementedException();
        }

        public ExceptionHandlerBuilder<T> Add(IExceptionHandler<T> handler)
        {
            throw new NotImplementedException();
        }
    }
}
