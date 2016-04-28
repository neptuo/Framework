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
	public class StringKeyToJTokenConverter : DefaultConverter<StringKey, JToken>
	{
		public override bool TryConvert(StringKey sourceValue, out JToken targetValue)
		{
			if (sourceValue == null)
			{
				targetValue = null;
				return false;
			}

			JObject result = new JObject();
			result["Type"] = sourceValue.Type;
			if (sourceValue.IsEmpty)
				result["Identifier"] = null;
			else
				result["Identifier"] = sourceValue.Identifier;

			targetValue = result;
			return true;
		}
	}
}
