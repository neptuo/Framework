using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.SharpKitCompilers
{
    [SharpKit.JavaScript.JsType(SharpKit.JavaScript.JsMode.Clr)]
    public class TestClass
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
