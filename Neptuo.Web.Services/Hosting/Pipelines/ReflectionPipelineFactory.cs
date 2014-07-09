using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Pipelines
{
    /// <summary>
    /// Creates instances of <see cref="ReflectionPipeline"/>.
    /// </summary>
    public class ReflectionPipelineFactory<T> : IPipelineFactory
        where T : new()
    {
        public IPipeline Create()
        {
            return new ReflectionPipeline<T>(ServiceEnvironment.Behaviors);
        }
    }
}
