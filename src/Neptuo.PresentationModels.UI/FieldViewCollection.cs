using Neptuo.PresentationModels;
using Neptuo;
using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpKit.JavaScript;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Implementation of <see cref="IFieldViewProvider{T}"/> based on model and field identifiers and field types, or "UI hint".
    /// Field view can be mapped using:
    /// - Field type.
    /// - Model and field identifier.
    /// - UI hint.
    /// </summary>
    public class FieldViewCollection<T> : IFieldViewProvider<T>
    {
        private readonly Dictionary<Key, IFactory<IFieldView<T>, IFieldDefinition>> builders
            = new Dictionary<Key, IFactory<IFieldView<T>, IFieldDefinition>>(new KeyComparer());

        private readonly OutFuncCollection<Key, IFactory<IFieldView<T>, IFieldDefinition>, bool> onSearchView
            = new OutFuncCollection<Key, IFactory<IFieldView<T>, IFieldDefinition>, bool>();

        public FieldViewCollection<T> Add(string modelIdentifier, string fieldIdentifier, string uiHint, Type fieldType, IFactory<IFieldView<T>, IFieldDefinition> fieldViewFactory)
        {
            Ensure.NotNull(fieldType, "fieldType");
            Ensure.NotNull(fieldViewFactory, "fieldViewFactory");
            builders[new Key(modelIdentifier, fieldIdentifier, uiHint, fieldType)] = fieldViewFactory;
            return this;
        }

        public FieldViewCollection<T> AddSearchHandler(OutFunc<Key, IFactory<IFieldView<T>, IFieldDefinition>, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");
            onSearchView.Add(searchHandler);
            return this;
        }

        public bool TryGet(IModelDefinition modelDefinition, IFieldDefinition fieldDefinition, out IFieldView<T> fieldView)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Ensure.NotNull(fieldDefinition, "fieldDefinition");

            IFactory<IFieldView<T>, IFieldDefinition> modelViewActivator;
            foreach (Key key in LazyEnumerateKeys(modelDefinition, fieldDefinition))
            {
                if (builders.TryGetValue(key, out modelViewActivator))
                {
                    fieldView = modelViewActivator.Create(fieldDefinition);
                    return true;
                }
            }

            foreach (Key key in LazyEnumerateKeys(modelDefinition, fieldDefinition, true))
            {
                if (onSearchView.TryExecute(key, out modelViewActivator))
                {
                    builders[key] = modelViewActivator;
                    fieldView = modelViewActivator.Create(fieldDefinition);
                    return true;
                }
            }

            fieldView = null;
            return false;
        }

        private IEnumerable<Key> LazyEnumerateKeys(IModelDefinition modelDefinition, IFieldDefinition fieldDefinition, bool isForSearchHandler = false)
        {
            string modelIdentifier = modelDefinition.Identifier;
            string fieldIdentifier = fieldDefinition.Identifier;
            Type fieldType = fieldDefinition.FieldType;
            
            string uiHint;
            if (!fieldDefinition.Metadata.TryGet("UiHint", out uiHint))
                uiHint = null;

            if (isForSearchHandler)
            {
                yield return new Key(modelIdentifier, fieldIdentifier, uiHint, fieldType);
            }
            else
            {
                yield return new Key(modelIdentifier, fieldIdentifier, null, fieldType);
                yield return new Key(modelIdentifier, null, uiHint, fieldType);
                yield return new Key(null, null, uiHint, fieldType);
                yield return new Key(null, null, null, fieldType);
            }
        }

        /// <summary>
        /// Key for field view registration.
        /// </summary>
        public class Key
        {
            /// <summary>
            /// Model identifier.
            /// </summary>
            public string ModelIdentifier { get; private set; }

            /// <summary>
            /// Field identifier.
            /// </summary>
            public string FieldIdentifier { get; private set; }

            /// <summary>
            /// UI hint.
            /// </summary>
            public string UiHint { get; private set; }

            /// <summary>
            /// Field type.
            /// </summary>
            public Type FieldType { get; private set; }
            
            /// <summary>
            /// Creates new instance.
            /// </summary>
            /// <param name="modelIdentifier">Model identifier.</param>
            /// <param name="fieldIdentifier">Field identifier.</param>
            /// <param name="uiHint">UI hint.</param>
            /// <param name="fieldType">Field type.</param>
            public Key(string modelIdentifier, string fieldIdentifier, string uiHint, Type fieldType)
            {
                ModelIdentifier = modelIdentifier;
                FieldIdentifier = fieldIdentifier;
                UiHint = uiHint;
                FieldType = fieldType;
            }

            public override int GetHashCode()
            {
                int value = 13;
                if (ModelIdentifier != null)
                    value ^= 3 ^ ModelIdentifier.GetHashCode();

                if (FieldIdentifier != null)
                    value ^= 5 ^ FieldIdentifier.GetHashCode();

                if (UiHint != null)
                    value ^= 7 ^ UiHint.GetHashCode();

                if (FieldType != null)
                    value ^= 11 ^ GetTypeHashCode(FieldType);

                return value;
            }

            private int GetTypeHashCode(Type type)
            {
                int value = type.FullName.GetHashCode();

                if (type.IsGenericType)
                {
                    foreach (Type argument in type.GetGenericArguments())
                        value ^= GetTypeHashCode(argument);
                }

                return value;
            }

            public override bool Equals(object obj)
            {
                Key other = obj as Key;
                if (other == null)
                    return false;

                if (ModelIdentifier != other.ModelIdentifier)
                    return false;

                if (FieldIdentifier != other.FieldIdentifier)
                    return false;

                if (UiHint != other.UiHint)
                    return false;

                if (FieldType != other.FieldType)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// Because of SharpKit and dictionary support.
        /// </summary>
        private class KeyComparer : IEqualityComparer<Key>
        {
            public bool Equals(Key x, Key y)
            {
                if (x == null)
                    return y == null;

                if (y == null)
                    return false;

                return x.Equals(y);
            }

            [JsMethod(Name = "GetHashCode$$T", NativeOverloads = true)]
            public int GetHashCode(Key obj)
            {
                return obj.GetHashCode();
            }
        }

    }
}
