using Neptuo.Converters;
using Neptuo.Models.Keys;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Converters
{
    /// <summary>
    /// The extensions for registering converters between keys and JSON.
    /// </summary>
    public static class _ConverterRepositoryExtensions
    {
        /// <summary>
        /// Registers conversion from/to <see cref="GuidKey"/>, <see cref="StringKey"/> and <see cref="IKey"/> (of type <see cref="GuidKey"/> or <see cref="StringKey"/>) and <see cref="JToken"/>.
        /// </summary>
        /// <param name="repository">The repository to register converters.</param>
        /// <returns>Self (for fluency).</returns>
        public static IConverterRepository AddKeyToJson(this IConverterRepository repository)
        {
            Ensure.NotNull(repository, "repository");

            GuidKeyToJTokenConverter guidConverter = new GuidKeyToJTokenConverter();
            StringKeyToJTokenConverter stringConverter = new StringKeyToJTokenConverter();
            KeyToJTokenConverter keyConverter = new KeyToJTokenConverter();

            return repository
                .Add<GuidKey, JToken>(guidConverter)
                .Add<JToken, GuidKey>(guidConverter)
                .Add<StringKey, JToken>(stringConverter)
                .Add<JToken, StringKey>(stringConverter)
                .Add<IKey, JToken>(keyConverter)
                .Add<JToken, IKey>(keyConverter);
        }
    }
}
