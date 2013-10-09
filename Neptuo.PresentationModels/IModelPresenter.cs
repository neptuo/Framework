using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public interface IModelPresenter
    {
        IModelPresenter SetModel(IModelDefinition modelDefinition);
        IModelPresenter SetView(IModelView modelView);
        void Prepare();

        void SetData(IModelValueGetter getter);
        void GetData(IModelValueSetter setter);
    }
}
