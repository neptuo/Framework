using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// Common extensions for <see cref="XmlTypeMappingCollection"/>.
    /// </summary>
    public static class _XmlTypeMappingCollectionExtensions
    {
        /// <summary>
        /// Maps all C# keywords base types and its nullable counter parts.
        /// 'int', 'string', 'bool', 'long', 'double', 'decimal' and all these with '?'.
        /// </summary>
        /// <param name="collection">Target collection of mappings.</param>
        /// <returns>Modified <paramref name="collection"/>.</returns>
        public static XmlTypeMappingCollection AddStandartKeywords(this XmlTypeMappingCollection collection)
        {
            Ensure.NotNull(collection, "collection");
            return collection
                .Add("string", typeof(String))
                .Add("int", typeof(Int32))
                .Add("bool", typeof(Boolean))
                .Add("long", typeof(Int64))
                .Add("double", typeof(Double))
                .Add("decimal", typeof(Decimal))

                .Add("int?", typeof(Int32?))
                .Add("bool?", typeof(Boolean?))
                .Add("long?", typeof(Int64?))
                .Add("double?", typeof(Double?))
                .Add("decimal?", typeof(Decimal?));
        }

        /// <summary>
        /// Maps XML name <paramref name="xmlName"/> to the .NET type <typeparamref name="T"/> and vice versa.
        /// </summary>
        /// <typeparam name="T">.NET type.</typeparam>
        /// <param name="collection">Target collection of mappings.</param>
        /// <param name="xmlName">XML type name.</param>
        /// <returns>Self (for fluency).</returns>
        public static XmlTypeMappingCollection Add<T>(this XmlTypeMappingCollection collection, string xmlName)
        {
            Ensure.NotNull(collection, "collection");
            return collection.Add(xmlName, typeof(T));
        }
    }
}
