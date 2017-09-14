using Neptuo;
using Neptuo.Collections.Specialized;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// Key registration extensions for the <see cref="KeyToParametersConverter"/> and <see cref="KeyToParametersConverter.MappingCollection"/>.
    /// </summary>
    public static class _KeyToParametersConverterMappingExtensions
    {
        /// <summary>
        /// Adds default mappings to the <paramref name="converter"/> for <see cref="Int16Key"/>, <see cref="Int32Key"/>, <see cref="Int64Key"/>, <see cref="StringKey"/> and <see cref="GuidKey"/>.
        /// </summary>
        /// <param name="converter">A key-parameters converter.</param>
        /// <returns><paramref name="converter"/>.</returns>
        public static KeyToParametersConverter AddDefaultMapping(this KeyToParametersConverter converter)
        {
            Ensure.NotNull(converter, "converter");
            converter.Definitions.AddDefaultMapping();
            return converter;
        }

        /// <summary>
        /// Adds default mappings to the <paramref name="converter"/> for <see cref="Int16Key"/>, <see cref="Int32Key"/>, <see cref="Int64Key"/>, <see cref="StringKey"/> and <see cref="GuidKey"/>.
        /// </summary>
        /// <param name="converter">A key-parameters converter definitions.</param>
        /// <returns><paramref name="definitions"/>.</returns>
        public static KeyToParametersConverter.MappingCollection AddDefaultMapping(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions
                .AddInt16KeyToParameters()
                .AddInt32KeyToParameters()
                .AddInt64KeyToParameters()
                .AddStringKeyToParameters()
                .AddGuidKeyToParameters()
                .AddParametersToInt16Key()
                .AddParametersToInt32Key()
                .AddParametersToInt64Key()
                .AddParametersToStringKey()
                .AddParametersToGuidKey();
        }

        #region Add key to parameters

        /// <summary>
        /// Adds mapping from <see cref="Int16Key"/> to parameters.
        /// </summary>
        /// <param name="converter">A key-parameters converter definitions.</param>
        /// <returns><paramref name="definitions"/>.</returns>
        public static KeyToParametersConverter.MappingCollection AddInt16KeyToParameters(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddKeyToParameters<Int16Key>((values, key) => values.Add("ID", key.ID).Add("Type", key.Type));
        }

        /// <summary>
        /// Adds mapping from <see cref="Int32Key"/> to parameters.
        /// </summary>
        /// <param name="converter">A key-parameters converter definitions.</param>
        /// <returns><paramref name="definitions"/>.</returns>
        public static KeyToParametersConverter.MappingCollection AddInt32KeyToParameters(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddKeyToParameters<Int32Key>((values, key) => values.Add("ID", key.ID).Add("Type", key.Type));
        }

        /// <summary>
        /// Adds mapping from <see cref="Int64Key"/> to parameters.
        /// </summary>
        /// <param name="converter">A key-parameters converter definitions.</param>
        /// <returns><paramref name="definitions"/>.</returns>
        public static KeyToParametersConverter.MappingCollection AddInt64KeyToParameters(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddKeyToParameters<Int64Key>((values, key) => values.Add("ID", key.ID).Add("Type", key.Type));
        }

        /// <summary>
        /// Adds mapping from <see cref="StringKey"/> to parameters.
        /// </summary>
        /// <param name="converter">A key-parameters converter definitions.</param>
        /// <returns><paramref name="definitions"/>.</returns>
        public static KeyToParametersConverter.MappingCollection AddStringKeyToParameters(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddKeyToParameters<StringKey>((values, key) => values.Add("Identifier", key.Identifier).Add("Type", key.Type));
        }

        /// <summary>
        /// Adds mapping from <see cref="GuidKey"/> to parameters.
        /// </summary>
        /// <param name="converter">A key-parameters converter definitions.</param>
        /// <returns><paramref name="definitions"/>.</returns>
        public static KeyToParametersConverter.MappingCollection AddGuidKeyToParameters(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddKeyToParameters<GuidKey>((values, key) => values.Add("Guid", key.Guid.ToString()).Add("Type", key.Type));
        }

        #endregion

        #region Get key from parameters

        /// <summary>
        /// Adds mapping from parameters to <see cref="Int16Key"/>.
        /// </summary>
        /// <param name="converter">A key-parameters converter definitions.</param>
        /// <returns><paramref name="definitions"/>.</returns>
        public static KeyToParametersConverter.MappingCollection AddParametersToInt16Key(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddParametersToKey<Int16Key>(TryGetInt16Key);
        }

        /// <summary>
        /// Adds mapping from parameters to <see cref="Int32Key"/>.
        /// </summary>
        /// <param name="converter">A key-parameters converter definitions.</param>
        /// <returns><paramref name="definitions"/>.</returns>
        public static KeyToParametersConverter.MappingCollection AddParametersToInt32Key(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddParametersToKey<Int32Key>(TryGetInt32Key);
        }

        /// <summary>
        /// Adds mapping from parameters to <see cref="Int64Key"/>.
        /// </summary>
        /// <param name="converter">A key-parameters converter definitions.</param>
        /// <returns><paramref name="definitions"/>.</returns>
        public static KeyToParametersConverter.MappingCollection AddParametersToInt64Key(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddParametersToKey<Int64Key>(TryGetInt64Key);
        }

        /// <summary>
        /// Adds mapping from parameters to <see cref="StringKey"/>.
        /// </summary>
        /// <param name="converter">A key-parameters converter definitions.</param>
        /// <returns><paramref name="definitions"/>.</returns>
        public static KeyToParametersConverter.MappingCollection AddParametersToStringKey(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddParametersToKey<StringKey>(TryGetStringKey);
        }

        /// <summary>
        /// Adds mapping from parameters to <see cref="GuidKey"/>.
        /// </summary>
        /// <param name="converter">A key-parameters converter definitions.</param>
        /// <returns><paramref name="definitions"/>.</returns>
        public static KeyToParametersConverter.MappingCollection AddParametersToGuidKey(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddParametersToKey<GuidKey>(TryGetGuidKey);
        }

        private static bool TryGetInt16Key(IReadOnlyKeyValueCollection parameters, out Int16Key key)
        {
            if (parameters.TryGet("Type", out string type) && parameters.TryGet("ID", out short id))
            {
                key = Int16Key.Create(id, type);
                return true;
            }

            key = null;
            return false;
        }

        private static bool TryGetInt32Key(IReadOnlyKeyValueCollection parameters, out Int32Key key)
        {
            if (parameters.TryGet("Type", out string type) && parameters.TryGet("ID", out int id))
            {
                key = Int32Key.Create(id, type);
                return true;
            }

            key = null;
            return false;
        }

        private static bool TryGetInt64Key(IReadOnlyKeyValueCollection parameters, out Int64Key key)
        {
            if (parameters.TryGet("Type", out string type) && parameters.TryGet("ID", out long id))
            {
                key = Int64Key.Create(id, type);
                return true;
            }

            key = null;
            return false;
        }

        private static bool TryGetStringKey(IReadOnlyKeyValueCollection parameters, out StringKey key)
        {
            if (parameters.TryGet("Type", out string type) && parameters.TryGet("Identifier", out string identifier))
            {
                key = StringKey.Create(identifier, type);
                return true;
            }

            key = null;
            return false;
        }

        private static bool TryGetGuidKey(IReadOnlyKeyValueCollection parameters, out GuidKey key)
        {
            if (parameters.TryGet("Type", out string type) && parameters.TryGet("Guid", out Guid guid))
            {
                key = GuidKey.Create(guid, type);
                return true;
            }

            key = null;
            return false;
        }

        #endregion
    }
}
