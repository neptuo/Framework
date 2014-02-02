using SharpKit.JavaScript;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Client.System.IO
{
    [JsType(Name = "System.IO.StringWriter", Filename = "~/res/System.IO.js")]
    public class JsImplStringWriter
    {
        private JsArray<string> array;

        public int Length { get; private set; }

        public JsImplStringWriter()
        {
            array = new JsArray<string>();
        }

        public JsImplStringWriter(string s)
        {
            array = new JsArray<string>(s);
            Length = s == null ? 0 : s.Length;
        }

        public void Write(char s)
        {
            Length++;
            array.push(s.As<string>());
        }

        public void Write(string s)
        {
            if (s != null)
            {
                array.push(s);
                Length += s.Length;
            }
        }

        public void Write(object obj)
        {
            if (obj != null)
            {
                string s = obj.ToString();
                array.push(s);
                Length += s.Length;
            }
        }

        [JsMethod(Name = "Append$$Object")]
        public void Append(object obj)
        {
            if (obj != null)
            {
                string s = obj.ToString();
                array.push(s);
                Length += s.Length;
            }
        }

        public override string ToString()
        {
            return array.join("");
        }
    }
}
