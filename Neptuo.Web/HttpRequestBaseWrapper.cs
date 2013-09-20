using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Neptuo.Web
{
    public class HttpRequestBaseWrapper : HttpRequestBase
    {
        /// <summary>
        /// Wrapped <see cref="HttpRequestBase"/>.
        /// </summary>
        protected HttpRequestBase HttpRequestBase { get; private set; }

        public override void Abort()
        {
            HttpRequestBase.Abort();
        }

        public override string[] AcceptTypes
        {
            get { return HttpRequestBase.AcceptTypes; }
        }

        public override string AnonymousID
        {
            get { return HttpRequestBase.AnonymousID; }
        }

        public override string ApplicationPath
        {
            get { return HttpRequestBase.ApplicationPath; }
        }

        public override byte[] BinaryRead(int count)
        {
            return HttpRequestBase.BinaryRead(count);
        }

        public override string AppRelativeCurrentExecutionFilePath
        {
            get { return HttpRequestBase.AppRelativeCurrentExecutionFilePath; }
        }

        public override HttpBrowserCapabilitiesBase Browser
        {
            get { return HttpRequestBase.Browser; }
        }

        public override HttpClientCertificate ClientCertificate
        {
            get { return HttpRequestBase.ClientCertificate; }
        }

        public override System.Text.Encoding ContentEncoding
        {
            get { return HttpRequestBase.ContentEncoding; }
            set { HttpRequestBase.ContentEncoding = value; }
        }

        public override int ContentLength
        {
            get { return HttpRequestBase.ContentLength; }
        }

        public override string ContentType
        {
            get { return HttpRequestBase.ContentType; }
            set { HttpRequestBase.ContentType = value; }
        }

        public override HttpCookieCollection Cookies
        {
            get { return HttpRequestBase.Cookies; }
        }

        public override string CurrentExecutionFilePath
        {
            get { return HttpRequestBase.CurrentExecutionFilePath; }
        }

        public override string CurrentExecutionFilePathExtension
        {
            get { return HttpRequestBase.CurrentExecutionFilePathExtension; }
        }

        public override bool Equals(object obj)
        {
            return HttpRequestBase.Equals(obj);
        }

        public override string FilePath
        {
            get { return HttpRequestBase.FilePath; }
        }

        public override HttpFileCollectionBase Files
        {
            get { return HttpRequestBase.Files; }
        }

        public override Stream Filter
        {
            get { return HttpRequestBase.Filter; }
            set { HttpRequestBase.Filter = value; }
        }

        public override NameValueCollection Form
        {
            get { return HttpRequestBase.Form; }
        }

        public override Stream GetBufferedInputStream()
        {
            return HttpRequestBase.GetBufferedInputStream();
        }

        public override Stream GetBufferlessInputStream()
        {
            return HttpRequestBase.GetBufferlessInputStream();
        }

        public override Stream GetBufferlessInputStream(bool disableMaxRequestLength)
        {
            return HttpRequestBase.GetBufferlessInputStream(disableMaxRequestLength);
        }

        public override int GetHashCode()
        {
            return HttpRequestBase.GetHashCode();
        }

        public override NameValueCollection Headers
        {
            get { return HttpRequestBase.Headers; }
        }

        public override ChannelBinding HttpChannelBinding
        {
            get { return HttpRequestBase.HttpChannelBinding; }
        }

        public override string HttpMethod
        {
            get { return HttpRequestBase.HttpMethod; }
        }

        public override Stream InputStream
        {
            get { return HttpRequestBase.InputStream; }
        }

        public override void InsertEntityBody()
        {
            HttpRequestBase.InsertEntityBody();
        }

        public override void InsertEntityBody(byte[] buffer, int offset, int count)
        {
            HttpRequestBase.InsertEntityBody(buffer, offset, count);
        }

        public override bool IsAuthenticated
        {
            get { return HttpRequestBase.IsAuthenticated; }
        }

        public override bool IsLocal
        {
            get { return HttpRequestBase.IsLocal; }
        }

        public override bool IsSecureConnection
        {
            get { return HttpRequestBase.IsSecureConnection; }
        }

        public override WindowsIdentity LogonUserIdentity
        {
            get { return HttpRequestBase.LogonUserIdentity; }
        }

        public override int[] MapImageCoordinates(string imageFieldName)
        {
            return HttpRequestBase.MapImageCoordinates(imageFieldName);
        }

        public override string MapPath(string virtualPath)
        {
            return HttpRequestBase.MapPath(virtualPath);
        }

        public override string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
        {
            return HttpRequestBase.MapPath(virtualPath, baseVirtualDir, allowCrossAppMapping);
        }

        public override double[] MapRawImageCoordinates(string imageFieldName)
        {
            return HttpRequestBase.MapRawImageCoordinates(imageFieldName);
        }

        public override NameValueCollection Params
        {
            get { return HttpRequestBase.Params; }
        }

        public override string Path
        {
            get { return HttpRequestBase.Path; }
        }

        public override string PathInfo
        {
            get { return HttpRequestBase.PathInfo; }
        }

        public override string PhysicalApplicationPath
        {
            get { return HttpRequestBase.PhysicalApplicationPath; }
        }

        public override string PhysicalPath
        {
            get { return HttpRequestBase.PhysicalPath; }
        }

        public override NameValueCollection QueryString
        {
            get { return HttpRequestBase.QueryString; }
        }

        public override string RawUrl
        {
            get { return HttpRequestBase.RawUrl; }
        }

        public override ReadEntityBodyMode ReadEntityBodyMode
        {
            get { return HttpRequestBase.ReadEntityBodyMode; }
        }

        public override RequestContext RequestContext
        {
            get { return HttpRequestBase.RequestContext; }
            set { HttpRequestBase.RequestContext = value; }
        }

        public override string RequestType
        {
            get { return HttpRequestBase.RequestType; }
            set { HttpRequestBase.RequestType = value; }
        }

        public override void SaveAs(string filename, bool includeHeaders)
        {
            HttpRequestBase.SaveAs(filename, includeHeaders);
        }

        public override NameValueCollection ServerVariables
        {
            get { return HttpRequestBase.ServerVariables; }
        }

        public override string this[string key]
        {
            get { return HttpRequestBase[key]; }
        }

        public override CancellationToken TimedOutToken
        {
            get { return HttpRequestBase.TimedOutToken; }
        }

        public override string ToString()
        {
            return HttpRequestBase.ToString();
        }

        public override int TotalBytes
        {
            get { return HttpRequestBase.TotalBytes; }
        }

        public override UnvalidatedRequestValuesBase Unvalidated
        {
            get { return HttpRequestBase.Unvalidated; }
        }

        public override Uri Url
        {
            get { return HttpRequestBase.Url; }
        }

        public override Uri UrlReferrer
        {
            get { return HttpRequestBase.UrlReferrer; }
        }

        public override string UserAgent
        {
            get { return HttpRequestBase.UserAgent; }
        }

        public override string UserHostAddress
        {
            get { return HttpRequestBase.UserHostAddress; }
        }

        public override string UserHostName
        {
            get { return HttpRequestBase.UserHostName; }
        }

        public override string[] UserLanguages
        {
            get { return HttpRequestBase.UserLanguages; }
        }

        public override void ValidateInput()
        {
            HttpRequestBase.ValidateInput();
        }

        public HttpRequestBaseWrapper(HttpRequestBase httpRequestBase)
        {
            HttpRequestBase = httpRequestBase;
        }
    }

}
