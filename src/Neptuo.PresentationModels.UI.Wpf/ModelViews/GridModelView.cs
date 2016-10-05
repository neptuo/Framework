using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI.ModelViews
{
    public class GridModelView : ModelView<RenderContext>
    {
        private readonly IModelDefinition modelDefinition;

        public GridModelView(IModelDefinition modelDefinition)
            : base(modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            this.modelDefinition = modelDefinition;
        }

        protected override void RenderInternal(RenderContext context)
        {
            
        }
    }
}
