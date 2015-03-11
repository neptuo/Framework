using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpKit.JavaScript.Private
{
	[JsType(Name = "System.Environment", Filename = "~/Internal/Core.js")]
	class JsImplEnvironment
	{
		internal static string GetResourceString(string p)
		{
			return p;
		}

        private const string newLine = "\n\r";
        public static string NewLine { get { return newLine; } }
	}

}
