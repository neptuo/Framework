using Neptuo.Converters;
using Neptuo.Models.Keys;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Converters
{
    public class JTokenToKeyConverter : DefaultConverter<JToken, IKey>
	{
		public override bool TryConvert(JToken sourceValue, out IKey targetValue)
		{
			JObject source = sourceValue as JObject;
			if (source == null)
			{
				targetValue = null;
				return false;
			}

            if(source.Value)

			string type = source.Value<string>("Type");
			string value = source.Value<string>("Identifier");

			targetValue = StringKey.Create(value, type);
			return true;
		}
    }
}
