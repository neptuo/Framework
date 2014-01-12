using System;
using System.Collections.Generic;

using System.Text;


namespace SharpKit.JavaScript.Private
{

	[JsType(Name = "System.Uri", Filename = "~/Internal/Core.js")]
	internal class JsImplUri
	{
		public JsImplUri(string uri)
		{
			_OriginalString = uri;
		}

		string _OriginalString;
		public string OriginalString 
		{
			get
			{
				return _OriginalString;
			}
		}

		public override string ToString()
		{
			return _OriginalString;
		}
	}
}
