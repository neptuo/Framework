using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XSerializer = System.Xml.Serialization.XmlSerializer;

namespace Neptuo.Web.Services.Hosting.Http.Serialization
{
    /// <summary>
    /// Base class for serializing outputs and deserializing inputs using <see cref="System.Xml.Serialization.XmlSerializer"/>.
    /// </summary>
    public abstract class XmlSerializerBase : ISerializer, IDeserializer
    {
        /// <summary>
        /// Tries to serialize <paramref name="model"/> using <see cref="System.Xml.Serialization.XmlSerializer"/>.
        /// </summary>
        /// <typeparam name="T">Type of model to serialize.</typeparam>
        /// <param name="response">Current http response.</param>
        /// <param name="model">Model to serialize.</param>
        /// <returns><c>true</c> if serialization succeeds; false otherwise.</returns>
        public virtual bool TrySerialize<T>(IHttpResponse response, T model)
        {
            XSerializer serializer = new XSerializer(typeof(T));
            serializer.Serialize(response.Output, model);
            return true;
        }

        /// <summary>
        /// Tries to deserialize <paramref name="model"/> from <c>request.Input</c> using <see cref="System.Xml.Serialization.XmlSerializer"/>.
        /// </summary>
        /// <typeparam name="T">Type of model to deserialize.</typeparam>
        /// <param name="response">Current http response.</param>
        /// <param name="model">Deserialized model.</param>
        /// <returns><c>true</c> if deserialization succeeds; false otherwise.</returns>
        public virtual bool TryDeserialize<T>(IHttpRequest request, out T model)
        {
            XSerializer serializer = new XSerializer(typeof(T));
            model = (T)serializer.Deserialize(request.Input);
            return true;
        }
    }
}
