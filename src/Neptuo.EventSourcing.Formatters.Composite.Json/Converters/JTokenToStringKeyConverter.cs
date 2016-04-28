using Neptuo.Converters;
using Neptuo.Models.Keys;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkerMeet.Converters
{
	public class JTokenToStringKeyConverter : DefaultConverter<JToken, StringKey>
	{
		public override bool TryConvert(JToken sourceValue, out StringKey targetValue)
		{
			JObject source = sourceValue as JObject;
			if (source == null)
			{
				targetValue = null;
				return false;
			}

			string type = source.Value<string>("Type");
			string value = source.Value<string>("Identifier");

			targetValue = StringKey.Create(value, type);
			return true;
		}
	}
}
