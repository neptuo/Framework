using Neptuo;
using Neptuo.Web.Services.Hosting;
using Neptuo.Web.Services.Hosting.Behaviors;
using Neptuo.Web.Services.Hosting.Http;
using Neptuo.Web.Services.Hosting.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.WebServices
{
    public class ReflectionPipeline<T> : DefaultPipelineBase<T>
        where T : new()
    {
        private IBehaviorCollection collection;

        public ReflectionPipeline(IBehaviorCollection collection)
        {
            Guard.NotNull(collection, "collection");
            this.collection = collection;
        }

        protected override IEnumerable<IBehavior<T>> GetBehaviors(IHttpContext context)
        {
            IEnumerable<Type> behaviorTypes = collection.GetBehaviors(typeof(T));
            foreach (Type behaviorType in behaviorTypes)
                yield return (IBehavior<T>)Activator.CreateInstance(behaviorType);
        }
    }
}
