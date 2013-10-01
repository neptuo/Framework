using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public class DependencyQueryDispatcher : IQueryDispatcher
    {
        private IDependencyProvider dependencyProvider;

        public DependencyQueryDispatcher(IDependencyProvider dependencyProvider)
        {
            this.dependencyProvider = dependencyProvider;
        }

        public TQuery Get<TQuery>() 
            where TQuery : IQuery
        {
            TQuery query = dependencyProvider.Resolve<TQuery>();
            return query;
        }
    }
}
