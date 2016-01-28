using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Metadata
{
    /// <summary>
    /// The implementation of <see cref="ICompositeTypeProvider"/> which uses manual declarations for building a composite types.
    /// </summary>
    public class ManualCompositeTypeProvider : ICompositeTypeProvider
    {
        private readonly Dictionary<Type, CompositeType> storageByType = new Dictionary<Type, CompositeType>();
        private readonly Dictionary<string, CompositeType> storageByName = new Dictionary<string, CompositeType>();

        #region ICompositeTypeProvider

        public bool TryGet(Type type, out CompositeType definition)
        {
            Ensure.NotNull(type, "type");
            return storageByType.TryGetValue(type, out definition);
        }

        public bool TryGet(string typeName, out CompositeType definition)
        {
            Ensure.NotNullOrEmpty(typeName, "typeName");
            return storageByName.TryGetValue(typeName, out definition);
        }

        #endregion

        private readonly List<ManualCompositeTypeProvider.VersionBuilder> builders = new List<VersionBuilder>();

        public ManualCompositeTypeProvider.VersionBuilder<T> Add<T>(int version)
        {
            VersionBuilder<T> builder = new VersionBuilder<T>(version);
            builders.Add(builder);
            return builder;
        }

        public class VersionBuilder
        {
            internal Type Type { get; private set; }
            internal int Version { get; private set; }
            internal List<CompositeProperty> Properties { get; private set; }

            internal VersionBuilder(Type type, int version)
            {
                Type = type;
                Version = version;
                Properties = new List<CompositeProperty>();
            }
        }

        public class VersionBuilder<T> : VersionBuilder
        {
            internal VersionBuilder(int version)
                : base(typeof(T), version)
            { }

            public VersionBuilder<T> WithProperty<TValue>(Expression<Func<T, TValue>> getter)
            {
                Ensure.NotNull(getter, "getter");
                PropertyInfo propertyInfo = Type.GetProperty(TypeHelper.PropertyName(getter));
                if (propertyInfo == null)
                    throw Ensure.Exception.NotSupported();

                Properties.Add(new CompositeProperty(Properties.Count, propertyInfo.Name, propertyInfo.PropertyType, instance => propertyInfo.GetValue(instance)));
                return this;
            }

            public ManualCompositeTypeProvider WithConstructor(Expression<Func<T>> factory)
            {
                Ensure.NotNull(factory, "factory");
                throw new NotImplementedException();
            }
        }
    }
}
