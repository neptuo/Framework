using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http
{
    /// <summary>
    /// Describes Http response status.
    /// </summary>
    public class HttpStatus
    {
        /// <summary>
        /// Response status code.
        /// </summary>
        public int Code { get; private set; }

        /// <summary>
        /// Response status text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="code"/> and <paramref name="text"/>.
        /// </summary>
        /// <param name="code">Response status code.</param>
        /// <param name="text">Response status text.</param>
        public HttpStatus(int code, string text)
        {
            Guard.Positive(code, "code");
            Guard.NotNullOrEmpty(text, "text");
            Code = code;
            Text = text;
        }

        #region Object overriden methods

        public override bool Equals(object obj)
        {
            HttpStatus other = obj as HttpStatus;
            if (other == null)
                return false;

            return other.Code == Code && other.Text == Text;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode() ^ Text.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", Code, Text);
        }

        #endregion

        public static implicit operator HttpStatus(int code)
        {
            return KnownStatuses[code];
        }

        public static bool operator ==(HttpStatus left, HttpStatus right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HttpStatus left, HttpStatus right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// List known status codes.
        /// </summary>
        internal static HttpStatusCollection KnownStatuses = new HttpStatusCollection()
        {
            Ok,
            Created,
            NoContent,
            MovedPermanently,
            NotFound,
            InternalServerError
        };

        /// <summary>
        /// 200 OK
        /// </summary>
        public static HttpStatus Ok = new HttpStatus(200, "OK");

        /// <summary>
        /// 201 Created
        /// </summary>
        public static HttpStatus Created = new HttpStatus(201, "Created");

        /// <summary>
        /// 204 No Content
        /// </summary>
        public static HttpStatus NoContent = new HttpStatus(204, "No Content");

        /// <summary>
        /// 301 Moved Permanently.
        /// </summary>
        public static HttpStatus MovedPermanently = new HttpStatus(301, "Moved Permanently");

        /// <summary>
        /// 404 Not Found
        /// </summary>
        public static HttpStatus NotFound = new HttpStatus(404, "Not Found");

        /// <summary>
        /// 500, Internal Server Error
        /// </summary>
        public static HttpStatus InternalServerError = new HttpStatus(500, "Internal Server Error");
    }
}
