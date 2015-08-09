using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TestSharpKit.Handlers
{
    public class JavascriptHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string path = context.Request.QueryString["Path"];
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../artifacts/release/javascript/", path);

            if(File.Exists(filePath)) 
            {
                using (FileStream fileContent = new FileStream(filePath, FileMode.Open))
                {
                    context.Response.ContentType = "text/javascript";
                    fileContent.CopyTo(context.Response.OutputStream);
                }
            }
        }
    }
}