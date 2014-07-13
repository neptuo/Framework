using Neptuo.Web.Services.Hosting.Behaviors;
using Neptuo.Web.Services.Hosting.Http;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Pipelines
{
    public class CodeDomPipelineFactory : IPipelineFactory
    {
        private Type handlerType;
        private Func<IPipeline> generatedFactory;

        /// <summary>
        /// Behavior collection.
        /// </summary>
        private IBehaviorCollection behaviorCollection;

        public CodeDomPipelineFactory(Type handlerType, IBehaviorCollection behaviorCollection)
        {
            Guard.NotNull(handlerType, "handlerType");
            Guard.NotNull(behaviorCollection, "behaviorCollection");
            this.handlerType = handlerType;
            this.behaviorCollection = behaviorCollection;
        }

        public IPipeline Create()
        {
            EnsurePipelineFactory();
            return generatedFactory();
        }

        private void EnsurePipelineFactory()
        {
            if (generatedFactory == null)
                GeneratePipelineFactory();
        }

        private void GeneratePipelineFactory()
        {
            CodeDomPipelineGenerator generator = new CodeDomPipelineGenerator(handlerType, behaviorCollection);
            Type pipelineType = generator.GeneratePipeline();

            generatedFactory = () => (IPipeline)Activator.CreateInstance(pipelineType);
        }
    }
}
