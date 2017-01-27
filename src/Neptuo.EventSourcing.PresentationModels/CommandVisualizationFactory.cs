﻿using Neptuo;
using Neptuo.Activators;
using Neptuo.Commands;
using Neptuo.Converters;
using Neptuo.Formatters;
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
    /// A factory for visualizing commands.
    /// </summary>
    public class CommandVisualizationFactory : IFactory<ObjectVisualization, ICommand>
    {
        private readonly IDeserializer commandDeserializer;
        private readonly IConverterRepository converters;
        private readonly TypeModelDefinitionCollection modelDefinitionProvider;
        private readonly IFactory<IModelValueGetter, ICommand> modelValueGetterFactory;

        /// <summary>
        /// Creates a new instance with <see cref="Converts.Repository"/> and <see cref="ReflectionModelValueProvider"/>.
        /// </summary>
        /// <param name="commandDeserializer">A deserializer for command payload.</param>
        /// <param name="modelDefinitionProvider">A collection of presentation model definitions.</param>
        public CommandVisualizationFactory(IDeserializer commandDeserializer, TypeModelDefinitionCollection modelDefinitionProvider)
            : this(commandDeserializer, Converts.Repository, modelDefinitionProvider, Factory.Getter<IModelValueGetter, ICommand>(e => new ReflectionModelValueProvider(e)))
        { }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="commandDeserializer">A deserializer for command payload.</param>
        /// <param name="converters">A repository of converters.</param>
        /// <param name="modelDefinitionProvider">A collection of presentation model definitions.</param>
        /// <param name="modelValueGetterFactory">A factory for event value getter.</param>
        public CommandVisualizationFactory(IDeserializer commandDeserializer, IConverterRepository converters, TypeModelDefinitionCollection modelDefinitionProvider, IFactory<IModelValueGetter, ICommand> modelValueGetterFactory)
        {
            Ensure.NotNull(commandDeserializer, "eventDeserializer");
            Ensure.NotNull(converters, "converters");
            Ensure.NotNull(modelDefinitionProvider, "modelDefinitionProvider");
            Ensure.NotNull(modelValueGetterFactory, "modelValueGetterFactory");
            this.commandDeserializer = commandDeserializer;
            this.converters = converters;
            this.modelDefinitionProvider = modelDefinitionProvider;
            this.modelValueGetterFactory = modelValueGetterFactory;
        }

        public ObjectVisualization Create(ICommand command)
        {
            Ensure.NotNull(command, "command");
            IModelDefinition payloadDefinition;
            if (!modelDefinitionProvider.TryGet(command.GetType(), out payloadDefinition))
            {
                Debug.Fail(String.Format("Can't find a model definition for command '{0}'.", command.GetType()));
                return null;
            }

            IModelValueGetter paylodValueGetter = modelValueGetterFactory.Create(command);
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
