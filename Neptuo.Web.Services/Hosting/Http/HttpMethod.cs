using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http
{
    /// <summary>
    /// Describes Http method.
    /// </summary>
    public class HttpMethod
    {
        /// <summary>
        /// Http method name, should be upper case.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Whether request can contain body.
        /// </summary>
        public bool HasBody { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="name">Http method name.</param>
        /// <param name="hasBody">Whether request can contain body.</param>
        public HttpMethod(string name, bool hasBody)
        {
            Guard.NotNullOrEmpty(name, "name");
            Name = name.ToUpper();
            HasBody = hasBody;
        }

        #region Object overriden methods

        public override bool Equals(object obj)
        {
            HttpMethod other = obj as HttpMethod;
            if (other == null)
                return false;

            return other.Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("HttpMethod: {0}", Name);
        }

        #endregion

        #region Operators

        public static explicit operator HttpMethod(string standartName)
        {
            Guard.NotNullOrEmpty(standartName, "standartName");

            if (standartName == Get.Name)
                return Get;
            else if (standartName == Post.Name)
                return Post;
            else if (standartName == Put.Name)
                return Put;
            else if (standartName == Delete.Name)
                return Delete;
            else if (standartName == Head.Name)
                return Head;
            else if (standartName == Options.Name)
                return Options;
            else
                throw new ArgumentOutOfRangeException(String.Format("Passed non-standart http method name '{0}'"));
        }

        public static implicit operator string(HttpMethod method)
        {
            Guard.NotNull(method, "method");
            return method.Name;
        }

        #endregion

        /// <summary>
        /// Standart Http GET method.
        /// </summary>
        public static readonly HttpMethod Get = new HttpMethod("GET", false);

        /// <summary>
        /// Standart Http POST method.
        /// </summary>
        public static readonly HttpMethod Post = new HttpMethod("POST", true);

        /// <summary>
        /// Standart Http PUT method.
        /// </summary>
        public static readonly HttpMethod Put = new HttpMethod("PUT", true);

        /// <summary>
        /// Standart Http DELETE method.
        /// </summary>
        public static readonly HttpMethod Delete = new HttpMethod("DELETE", false);


        /// <summary>
        /// Standart Http HEAD method.
        /// </summary>
        public static readonly HttpMethod Head = new HttpMethod("HEAD", false);


        /// <summary>
        /// Standart Http OPTIONS method.
        /// </summary>
        public static readonly HttpMethod Options = new HttpMethod("OPTIONS", false);
    }
}
