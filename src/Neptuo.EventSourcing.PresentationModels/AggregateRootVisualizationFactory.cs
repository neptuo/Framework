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
    /// A factory for visualizing aggregate roots.
    /// </summary>
    public class AggregateRootVisualizationFactory : IFactory<AggregateRootVisualization, IKey>, IFactory<ObjectVisualization, IEvent>
    {
        private readonly IEventStore eventStore;
        private readonly IDeserializer eventDeserializer;
        private readonly IConverterRepository converters;
        private readonly TypeModelDefinitionCollection modelDefinitionProvider;
        private readonly IFactory<IModelValueGetter, IEvent> modelValueGetterFactory;

        /// <summary>
        /// Creates a new instance with <see cref="Converts.Repository"/> and <see cref="ReflectionModelValueProvider"/>.
        /// </summary>
        /// <param name="eventStore">A store of the event streams.</param>
        /// <param name="eventDeserializer">A deserializer for event payload.</param>
        /// <param name="modelDefinitionProvider">A collection of presentation model definitions.</param>
        public AggregateRootVisualizationFactory(IEventStore eventStore, IDeserializer eventDeserializer, TypeModelDefinitionCollection modelDefinitionProvider)
            : this(eventStore, eventDeserializer, Converts.Repository, modelDefinitionProvider, Factory.Getter<IModelValueGetter, IEvent>(e => new ReflectionModelValueProvider(e)))
        { }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="eventStore">A store of the event streams.</param>
        /// <param name="eventDeserializer">A deserializer for event payload.</param>
        /// <param name="converters">A repository of converters.</param>
        /// <param name="modelDefinitionProvider">A collection of presentation model definitions.</param>
        /// <param name="modelValueGetterFactory">A factory for event value getter.</param>
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

        /// <summary>
        /// Creates a visualization of an aggregate with the <paramref name="aggregateKey"/>.
        /// </summary>
        /// <param name="aggregateKey">A key of the agregate to visalize.</param>
        /// <returns>Creates a visualization of an aggregate with the <paramref name="aggregateKey"/>.</returns>
        public AggregateRootVisualization Create(IKey aggregateKey)
        {
            IEnumerable<EventModel> entities = eventStore.Get(aggregateKey);
            return Create(aggregateKey, entities);
        }

        /// <summary>
        /// Creates a visualization of an aggregate with the <paramref name="aggregateKey"/>.
        /// </summary>
        /// <param name="aggregateKey">A key of the agregate to visalize.</param>
        /// <param name="version">A last event version, that is skipped.</param>
        /// <returns>Creates a visualization of an aggregate with the <paramref name="aggregateKey"/>.</returns>
        public AggregateRootVisualization Create(IKey aggregateKey, int version)
        {
            IEnumerable<EventModel> entities = eventStore.Get(aggregateKey, version);
            return Create(aggregateKey, entities);
        }

        private AggregateRootVisualization Create(IKey aggregateKey, IEnumerable<EventModel> entities)
        {
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

        /// <summary>
        /// Creates a visualization of the <paramref name="payload"/>.
        /// </summary>
        /// <param name="payload">An event payload to visalize.</param>
        /// <returns>A visualization of the <paramref name="payload"/>.</returns>
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
