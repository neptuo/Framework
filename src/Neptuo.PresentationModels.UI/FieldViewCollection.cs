using Neptuo.PresentationModels;
using Neptuo;
using Neptuo.Activators;
using Neptuo.ComponentModel;
using Neptuo.Collections.Specialized;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpKit.JavaScript;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Kolekce aktivátorů <see cref="IFieldView"/>.
    /// </summary>
    /// <typeparam name="T">Typ kontextu pohledu.</typeparam>
    public class FieldViewCollection<T> : IFieldViewCollection<T>
    {
        private readonly Dictionary<Key, IActivator<IFieldView<T>, IFieldDefinition>> builders
            = new Dictionary<Key, IActivator<IFieldView<T>, IFieldDefinition>>(new KeyComparer());

        private readonly OutFuncCollection<Key, IActivator<IFieldView<T>, IFieldDefinition>, bool> onSearchView
            = new OutFuncCollection<Key, IActivator<IFieldView<T>, IFieldDefinition>, bool>();

        /// <summary>
        /// Spáruje <paramref name="modelIdentifier"/> s pohledem <paramref name="modelViewActivator"/>.
        /// </summary>
        /// <param name="modelIdentifier">Identifikátor definice modelu.</param>
        /// <param name="fieldIdentifier">Identifikátor fieldu.</param>
        /// <param name="uiHint">Ui hint.</param>
        /// <param name="fieldType">Type fieldu.</param>
        /// <param name="fieldViewActivator">Pohled, který se pro daný field má použít.</param>
        /// <returns>Sebe (kvůli fluentnosti).</returns>
        public FieldViewCollection<T> Add(string modelIdentifier, string fieldIdentifier, string uiHint, Type fieldType, IActivator<IFieldView<T>, IFieldDefinition> fieldViewActivator)
        {
            Ensure.NotNull(fieldType, "fieldType");
            Ensure.NotNull(fieldViewActivator, "fieldViewActivator");
            builders[new Key(modelIdentifier, fieldIdentifier, uiHint, fieldType)] = fieldViewActivator;
            return this;
        }

        /// <summary>
        /// Spáruje delegáta <paramref name="searchHandler" />, který bude spuštěn, 
        /// pokud se nepovedlo najít zaregistrovaný pohled pro model.
        /// </summary>
        /// <param name="searchHandler">Delegát, který může vrátit pohled.</param>
        /// <returns>Sebe (kvůli fluentnosti).</returns>
        public FieldViewCollection<T> AddSearchHandler(OutFunc<Key, IActivator<IFieldView<T>, IFieldDefinition>, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");
            onSearchView.Add(searchHandler);
            return this;
        }

        public bool TryGet(IModelDefinition modelDefinition, IFieldDefinition fieldDefinition, out IFieldView<T> fieldView)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Ensure.NotNull(fieldDefinition, "fieldDefinition");

            IActivator<IFieldView<T>, IFieldDefinition> modelViewActivator;
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
        /// Klíč pro registraci pohledu pro field.
        /// </summary>
        public class Key
        {
            /// <summary>
            /// Identifikátor definice modelu.
            /// </summary>
            public string ModelIdentifier { get; private set; }

            /// <summary>
            /// Identifikátor fieldu.
            /// </summary>
            public string FieldIdentifier { get; private set; }

            /// <summary>
            /// Ui hint.
            /// </summary>
            public string UiHint { get; private set; }

            /// <summary>
            /// Type fieldu.
            /// </summary>
            public Type FieldType { get; private set; }

            /// <summary>
            /// Vytvoří a nastaví instanci.
            /// </summary>
            /// <param name="modelIdentifier">Identifikátor definice modelu.</param>
            /// <param name="fieldIdentifier">Identifikátor fieldu.</param>
            /// <param name="uiHint">Ui hint.</param>
            /// <param name="fieldType">Type fieldu.</param>
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
        /// Protože SharpKit nechápe, jak správně zacházet se slovníkem 
        /// a výpočtem klíčů a podporuje pouze výpočet hash kódu přes komparátor.
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
