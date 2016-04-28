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
    public class JTokenToGuidKeyConverter : DefaultConverter<JToken, IKey>
    {
        public override bool TryConvert(JToken sourceValue, out IKey targetValue)
        {
            JObject source = sourceValue as JObject;
            if(source == null)
            {
                targetValue = null;
                return false;
            }

            string type = source.Value<string>("Type");
            string value = source.Value<string>("Guid");

            Guid guid;
            if(Guid.TryParse(value, out guid))
                targetValue = GuidKey.Create(guid, type);
            else
                targetValue = GuidKey.Empty(type);

            return true;
        }
    }
}
