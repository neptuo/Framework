using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Queries
{
    public class HttpQueryDispatcher : IQueryDispatcher
    {
        public Task<TOutput> QueryAsync<TOutput>(IQuery<TOutput> query)
        {
            throw new NotImplementedException();
        }
    }
}
