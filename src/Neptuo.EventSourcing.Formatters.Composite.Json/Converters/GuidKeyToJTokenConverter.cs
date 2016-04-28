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
    public class GuidKeyToJTokenConverter : DefaultConverter<GuidKey, JToken>
    {
        public override bool TryConvert(GuidKey sourceValue, out JToken targetValue)
        {
            if(sourceValue == null)
            {
                targetValue = null;
                return false;
            }

            JObject result = new JObject();
            result["Type"] = sourceValue.Type;
            if (sourceValue.IsEmpty)
                result["Guid"] = null;
            else
                result["Guid"] = sourceValue.Guid.ToString();

            targetValue = result;
            return true;
        }
    }
}
