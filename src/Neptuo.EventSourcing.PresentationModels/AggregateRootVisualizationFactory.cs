using Neptuo;
using Neptuo.Activators;
using Neptuo.Converters;
using Neptuo.Data;
using Neptuo.Events;
using Neptuo.Formatters;
using Neptuo.Models.Keys;
using Neptuo.PresentationModels.TypeModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// A component for visualizing aggregate roots.
    /// </summary>
    public class AggregateRootVisualizationFactory
    {
        private readonly IEventStore eventStore;
        private readonly IDeserializer eventDeserializer;
        private readonly IConverterRepository converters;
        private readonly TypeModelDefinitionCollection modelDefinitionProvider;
        private readonly IFactory<IModelValueGetter, IEvent> modelValueGetterFactory;

        public AggregateRootVisualizationFactory(IEventStore eventStore, IDeserializer eventDeserializer, IConverterRepository converters, TypeModelDefinitionCollection modelDefinitionProvider, IFactory<IModelValueGetter, IEvent> modelValueGetterFactory)
        {
            Ensure.NotNull(eventStore, "eventStore");
            Ensure.NotNull(eventDeserializer, "eventDeserializer");
            Ensure.NotNull(converters, "converters");
            Ensure.NotNull(modelDefinitionProvider, "modelDefinitionProvider");
            Ensure.NotNull(modelValueGetterFactory, "modelValueGetterFactory");
            this.eventStore = eventStore;
            this.eventDeserializer = eventDeserializer;
            this.converters = converters;
            this.modelDefinitionProvider = modelDefinitionProvider;
            this.modelValueGetterFactory = modelValueGetterFactory;
        }

        public AggregateRootVisualization Create(IKey aggregateKey)
        {
            IEnumerable<EventModel> entities = eventStore.Get(aggregateKey);
            List<ObjectVisualization> models = new List<ObjectVisualization>();
            foreach (EventModel entity in entities.OrderBy(e => e.Version))
            {
                object source = eventDeserializer.Deserialize(Type.GetType(entity.EventKey.Type), entity.Payload);

                IEvent payload = source as IEvent;
                if (payload == null)
                {
                    Envelope envelope = source as Envelope;
                    if (envelope != null)
                    {
                        payload = envelope.Body as IEvent;
                        if (payload == null)
                        {
                            Debug.Fail("Event is not an event.");
                            continue;
                        }
                    }
                }

                ObjectVisualization model = Create(payload);
                if (model == null)
                    continue;

                models.Add(model);
            }

            Type aggregateRootType = Type.GetType(aggregateKey.Type);
            IModelDefinition aggregateRootDefinition;
            if (modelDefinitionProvider.TryGet(aggregateRootType, out aggregateRootDefinition))
                return new AggregateRootVisualization(aggregateKey, aggregateRootDefinition, models);

            Debug.Fail(String.Format("Can't find a model definition for event '{0}'.", aggregateRootType));
            return null;
        }

        public ObjectVisualization Create(IEvent payload)
        {
            Ensure.NotNull(payload, "payload");
            IModelDefinition payloadDefinition;
            if (!modelDefinitionProvider.TryGet(payload.GetType(), out payloadDefinition))
            {
                Debug.Fail(String.Format("Can't find a model definition for event '{0}'.", payload.GetType()));
                return null;
            }

            IModelValueGetter paylodValueGetter = modelValueGetterFactory.Create(payload);
            DictionaryModelValueProvider stringValueGetter = new DictionaryModelValueProvider();

            foreach (IFieldDefinition fieldDefinition in payloadDefinition.Fields)
            {
                object fieldValue;
                if (paylodValueGetter.TryGetValue(fieldDefinition.Identifier, out fieldValue))
                {
                    object rawValue;
                    if (converters.TryConvert(fieldDefinition.FieldType, typeof(string), fieldValue, out rawValue))
                        stringValueGetter.TrySetValue(fieldDefinition.Identifier, rawValue);
                    else
                        stringValueGetter.TrySetValue(fieldDefinition.Identifier, fieldValue);
                }
            }

            ObjectVisualization model = new ObjectVisualization(payloadDefinition, stringValueGetter);
            return model;
        }
    }
}
