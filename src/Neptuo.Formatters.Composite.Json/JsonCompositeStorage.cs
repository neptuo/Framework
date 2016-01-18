using Neptuo.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// Newtonsoft.Json implementation of <see cref="ICompositeStorage"/>.
    /// </summary>
    public class JsonCompositeStorage : ICompositeStorage
    {
        private JObject root;

        private readonly JsonLoadSettings loadSettings;
        private readonly Formatting formatting;
        
        /// <summary>
        /// Creates new instance with default settings and <see cref="Formatting.None"/> for serialization.
        /// </summary>
        public JsonCompositeStorage()
            : this(new JsonLoadSettings())
        { }

        /// <summary>
        /// Creates new instance with <paramref name="settings" /> and <see cref="Formatting.None"/> for serialization.
        /// </summary>
        /// <param name="settings">The serializer and deserializer configuration.</param>
        public JsonCompositeStorage(JsonLoadSettings settings)
            : this(settings, Formatting.None)
        { }

        /// <summary>
        /// Create new instance with <paramref name="settings"/> and <paramref name="formatting"/>.
        /// </summary>
        /// <param name="settings">The serializer and deserializer configuration.</param>
        /// <param name="formatting">The indention formatting for serialization.</param>
        public JsonCompositeStorage(JsonLoadSettings loadSettings, Formatting formatting)
        {
            Ensure.NotNull(loadSettings, "loadSettings");
            this.loadSettings = loadSettings;
            this.formatting = formatting;
            root = new JObject();
        }

        private JsonCompositeStorage(JObject root, JsonLoadSettings loadSettings, Formatting formatting)
            : this(loadSettings, formatting)
        {
            Ensure.NotNull(root, "root");
            this.root = root;
        }

        public void Load(Stream input)
        {
            Ensure.NotNull(input, "input");

            using (StreamReader reader = new StreamReader(input))
                root = JObject.Parse(reader.ReadToEnd());
        }

        public void Store(Stream output)
        {
            Ensure.NotNull(output, "output");

            using (StreamWriter writer = new StreamWriter(output, Encoding.UTF8, 1024, true))
                writer.Write(root.ToString(formatting));
        }

        public IEnumerable<string> Keys
        {
            get
            {
                foreach (var item in root)
                    yield return item.Key;
            }
        }

        public ICompositeStorage Add(string key, object value)
        {
            root[key] = new JValue(value);
            return this;
        }

        public ICompositeStorage Add(string key)
        {
            JObject child = new JObject();
            root[key] = child;
            return new JsonCompositeStorage(child, loadSettings, formatting);
        }

        IKeyValueCollection IKeyValueCollection.Add(string key, object value)
        {
            return Add(key, value);
        }

        public bool TryGet<T>(string key, out T value)
        {
            JToken valueToken;
            JValue valueValue;
            if (root.TryGetValue(key, out valueToken) && (valueValue = valueToken as JValue) != null)
            {
                if (typeof(T) == typeof(object))
                    value = (T)valueValue.Value;
                else
                    value = valueValue.Value<T>();

                return true;
            }

            value = default(T);
            return false;
        }

        public bool TryGet(string key, out ICompositeStorage storage)
        {
            JToken valueToken;
            JObject valueObject;
            if (root.TryGetValue(key, out valueToken) && (valueObject = valueToken as JObject) != null)
            {
                storage = new JsonCompositeStorage(valueObject, loadSettings, formatting);
                return true;
            }

            storage = null;
            return false;
        }
    }
}
