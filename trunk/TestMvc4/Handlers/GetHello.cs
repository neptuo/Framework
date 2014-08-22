using Neptuo.Web.Services;
using Neptuo.Web.Services.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestMvc4.Handlers
{
    public class GetHello : IGet, IWithOutput<string>
    {
        public string Output { get; private set; }

        public void Execute()
        {
            Output = "Hello, World!";
        }
    }
}