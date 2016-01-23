using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Converters
{
    public class ModelCompositeConverter : CompositeConverterBase
    {
        protected override bool TryDeserialize(CompositeDeserializerContext context, out object value)
        {
            value = Activator.CreateInstance(context.ValueType);
            ICompositeModel compositeModel = value as ICompositeModel;
            if (compositeModel == null)
            {
                value = null;
                return false;
            }

            ICompositeStorage modelStorage;
            if (context.Storage.TryGet(context.Key, out modelStorage))
            {
                if (modelStorage == null)
                    value = null;
                else
                    compositeModel.Load(modelStorage);

                return true;
            }

            value = null;
            return false;
        }

        protected override bool TrySerialize(CompositeSerializerContext context)
        {
            if (context.Value == null)
            {
                context.Storage.Add(context.Key, null);
                return true;
            }

            ICompositeModel compositeModel = context.Value as ICompositeModel;
            if (compositeModel == null)
                return false;

            ICompositeStorage modelStorage = context.Storage.Add(context.Key);
            compositeModel.Save(modelStorage);
            return true;
        }
    }
}
