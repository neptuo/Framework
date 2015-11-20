using Neptuo.Queries.Handlers;
using Neptuo.Queries.Handlers.AutoExports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Queries
{
    public class Q1 : IQuery<R1>
    { }

    public class R1
    { }

    public class Q2 : IQuery<R2>
    { }

    public class R2
    { }

    public class Q3 : IQuery<R3>
    { }

    public class R3
    { }

    public class Q4 : IQuery<R4>
    { }

    public class R4
    { }

    [QueryHandler]
    class Q1Handler : IQueryHandler<Q1, R1>, IQueryHandler<Q2, R2>
    {
        public Task<R1> HandleAsync(Q1 query)
        {
            return Task.FromResult(new R1());
        }

        public Task<R2> HandleAsync(Q2 query)
        {
            return Task.FromResult(new R2());
        }
    }

    [QueryHandler(typeof(Q3))]
    class Q2Handler : IQueryHandler<Q3, R3>, IQueryHandler<Q4, R4>
    {
        public Task<R3> HandleAsync(Q3 query)
        {
            return Task.FromResult(new R3());
        }

        public Task<R4> HandleAsync(Q4 query)
        {
            return Task.FromResult(new R4());
        }
    }
}
