using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.Web.Services.Hosting.Http
{
    /// <summary>
    /// Wraps <see cref="HttpPostedFile"/>.
    /// </summary>
    public class AspNetHttpFile : IHttpFile
    {
        private readonly HttpPostedFile postedFile;

        public string FileName
        {
            get { return postedFile.FileName; }
        }

        public string ContentType
        {
            get { return postedFile.ContentType; }
        }

        public int ContentLength
        {
            get { return postedFile.ContentLength; }
        }

        public Stream Content
        {
            get { return postedFile.InputStream; }
        }

        public AspNetHttpFile(HttpPostedFile postedFile)
        {
            Guard.NotNull(postedFile, "postedFile");
            this.postedFile = postedFile;
        }
    }
}
