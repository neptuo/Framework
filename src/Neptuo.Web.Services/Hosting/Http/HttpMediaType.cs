using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http
{
    /// <summary>
    /// Describes media type of request and response.
    /// </summary>
    public class HttpMediaType
    {
        /// <summary>
        /// Text value of content type.
        /// </summary>
        /// <example>
        /// application/json
        /// </example>
        public string TextValue { get; private set; }

        /// <summary>
        /// Enumeration of supported text values.
        /// </summary>
        public ICollection<string> SupportedTextValues { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="textValue"/> as main media type 
        /// and optional enumeration of other matching media types.
        /// </summary>
        /// <param name="textValue">Text value of content type.</param>
        /// <param name="supportedTextValues">Enumeration of optional supported text values.</param>
        public HttpMediaType(string textValue, params string[] supportedTextValues)
        {
            Guard.NotNull(textValue, "textValue");
            TextValue = textValue;

            if (supportedTextValues != null)
                SupportedTextValues = new HashSet<string>(supportedTextValues);
            else
                SupportedTextValues = new HashSet<string>();

            SupportedTextValues.Add(TextValue);
        }

        /// <summary>
        /// Xhtml media type.
        /// </summary>
        public static HttpMediaType Xhtml = new HttpMediaType("application/xhtml+xml", "application/xhtml");

        /// <summary>
        /// Html media type.
        /// </summary>
        public static HttpMediaType Html = new HttpMediaType("text/html");

        /// <summary>
        /// Xml media type.
        /// </summary>
        public static HttpMediaType Xml = new HttpMediaType("application/xml", "text/xml");

        /// <summary>
        /// Json media type.
        /// </summary>
        public static HttpMediaType Json = new HttpMediaType("application/json", "text/json");
    }
}
