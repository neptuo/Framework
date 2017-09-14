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
    /// Key registration extensions for the <see cref="KeyToParametersConverter"/>.
    /// </summary>
    public static class _KeyToParametersConverterExtensions
    {
        public static KeyToParametersConverter AddDefaultMapping(this KeyToParametersConverter converter)
        {
            Ensure.NotNull(converter, "converter");
            converter.Definitions.AddDefaultMapping();
            return converter;
        }

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

        public static KeyToParametersConverter.MappingCollection AddInt16KeyToParameters(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddKeyToParameters<Int16Key>((values, key) => values.Add("ID", key.ID).Add("Type", key.Type));
        }

        public static KeyToParametersConverter.MappingCollection AddInt32KeyToParameters(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddKeyToParameters<Int32Key>((values, key) => values.Add("ID", key.ID).Add("Type", key.Type));
        }

        public static KeyToParametersConverter.MappingCollection AddInt64KeyToParameters(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddKeyToParameters<Int64Key>((values, key) => values.Add("ID", key.ID).Add("Type", key.Type));
        }

        public static KeyToParametersConverter.MappingCollection AddStringKeyToParameters(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddKeyToParameters<StringKey>((values, key) => values.Add("Identifier", key.Identifier).Add("Type", key.Type));
        }

        public static KeyToParametersConverter.MappingCollection AddGuidKeyToParameters(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddKeyToParameters<GuidKey>((values, key) => values.Add("Guid", key.Guid.ToString()).Add("Type", key.Type));
        }

        public static KeyToParametersConverter.MappingCollection AddParametersToInt16Key(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddParametersToKey<Int16Key>(TryGetInt16Key);
        }

        public static KeyToParametersConverter.MappingCollection AddParametersToInt32Key(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddParametersToKey<Int32Key>(TryGetInt32Key);
        }

        public static KeyToParametersConverter.MappingCollection AddParametersToInt64Key(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddParametersToKey<Int64Key>(TryGetInt64Key);
        }

        public static KeyToParametersConverter.MappingCollection AddParametersToStringKey(this KeyToParametersConverter.MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            return definitions.AddParametersToKey<StringKey>(TryGetStringKey);
        }

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

    }
}
