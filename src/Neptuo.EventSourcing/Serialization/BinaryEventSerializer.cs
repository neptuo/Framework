using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Serialization
{
    public class BinaryEventSerializer : IEventSerializer
    {
        public string Serialize(object payload)
        {
            using (MemoryStream content = new MemoryStream())
            using (StreamReader reader = new StreamReader(content))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(content, payload);
                content.Seek(0, SeekOrigin.Begin);

                string value = reader.ReadToEnd();
                return value;
            }
        }

        public object Deserialize(string payload)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(payload)))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream);
            }
        }
    }
}
