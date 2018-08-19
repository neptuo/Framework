using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using Neptuo.TypeMapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Generics
{
    /// <summary>
    /// An implementation of <see cref="IFormatter"/> which works on types implementing <see cref="ICompositeModel"/>.
    /// </summary>
    public class CompositeModelFormatter : IFormatter, ISerializer, IDeserializer
    {
        /// <summary>
        /// Nnames of the keys used in store/load methods.
        /// </summary>
        protected static class Name
        {
            /// <summary>
            /// Gets a name of the key used for storing a name of the composite type.
            /// </summary>
            public const string TypeName = "Name";

            /// <summary>
            /// Gets a name of the key used for storing values of model.
            /// </summary>
            public const string Payload = "Payload";
        }

        private readonly Func<Type, object> modelFactory;
        private readonly IFactory<ICompositeStorage> storageFactory;
        private readonly ITypeNameMapper typeNameMapper;

        /// <summary>
        /// Creates a new instance with factories from model and storages.
        /// </summary>
        /// <param name="modelFactory">A factory for empty models when deserializing.</param>
        /// <param name="storageFactory">A factory for empty composite storages.</param>
        /// <param name="typeNameMapper">A type name mapper.</param>
        public CompositeModelFormatter(Func<Type, object> modelFactory, IFactory<ICompositeStorage> storageFactory, ITypeNameMapper typeNameMapper)
        {
            Ensure.NotNull(modelFactory, "modelFactory");
            Ensure.NotNull(storageFactory, "storageFactory");
            Ensure.NotNull(typeNameMapper, "typeNameMapper");
            this.modelFactory = modelFactory;
            this.storageFactory = storageFactory;
            this.typeNameMapper = typeNameMapper;
        }

        private ICompositeStorage TrySerializeInternal(object input, ISerializerContext context)
        {
            Ensure.NotNull(input, "input");
            Ensure.NotNull(context, "context");

            ICompositeModel model = input as ICompositeModel;
            if (model == null)
                return null;

            ICompositeStorage storage = storageFactory.Create();

            if (!typeNameMapper.TryGet(model.GetType(), out string typeName))
                return null;

            storage.Add(Name.TypeName, typeName);
            ICompositeStorage childStorage = storage.Add(Name.Payload);

            model.Save(childStorage);
            return storage;
        }

        public async Task<bool> TrySerializeAsync(object input, ISerializerContext context)
        {
            ICompositeStorage storage = TrySerializeInternal(input, context);
            if (storage == null)
                return false;

            await storage.StoreAsync(context.Output).ConfigureAwait(false);
            return true;
        }

        public bool TrySerialize(object input, ISerializerContext context)
        {
            ICompositeStorage storage = TrySerializeInternal(input, context);
            if (storage == null)
                return false;

            storage.Store(context.Output);
            return true;
        }

        private bool TryDeserializeInternal(IDeserializerContext context, ICompositeStorage storage)
        {
            string typeName = storage.Get<string>(Name.TypeName);
            if (!typeNameMapper.TryGet(typeName, out Type outputType))
                return false;

            if (!storage.TryGet(Name.Payload, out ICompositeStorage childStorage))
                return false;

            ICompositeModel model = modelFactory.Invoke(outputType) as ICompositeModel;
            if (model == null)
                return false;

            model.Load(childStorage);
            context.Output = model;
            return true;
        }
        
        public async Task<bool> TryDeserializeAsync(Stream input, IDeserializerContext context)
        {
            Ensure.NotNull(input, "input");
            Ensure.NotNull(context, "context");

            ICompositeStorage storage = storageFactory.Create();
            await storage.LoadAsync(input).ConfigureAwait(false);

            return TryDeserializeInternal(context, storage);
        }

        public bool TryDeserialize(Stream input, IDeserializerContext context)
        {
            Ensure.NotNull(input, "input");
            Ensure.NotNull(context, "context");

            ICompositeStorage storage = storageFactory.Create();
            storage.Load(input);

            return TryDeserializeInternal(context, storage);
        }
    }
}
