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
    /// <summary>
    /// The converter from the <see cref="JToken"/> to the <see cref="IKey"/>.
    /// </summary>
    public class KeyToJTokenConverter : KeyToJObjectConverter<IKey>
	{
        protected override bool TryConvert(IKey source, out JObject target)
        {
            throw new NotImplementedException();
        }

        protected override bool TryConvert(JObject source, out IKey target)
        {
            target = null;

            if (source.GetValue(JsonName.StringValue) != null)
            {
                StringKey stringKey;
                if (Converts.Try<JToken, StringKey>(source, out stringKey))
                    target = stringKey;
            }
            else if (source.GetValue(JsonName.GuidValue) != null)
            {
                GuidKey guidKey;
                if (Converts.Try<JToken, GuidKey>(source, out guidKey))
                    target = guidKey;
            }

            return target != null;
        }
    }
}
