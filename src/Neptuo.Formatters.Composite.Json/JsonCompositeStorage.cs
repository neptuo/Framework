﻿using Newtonsoft.Json;
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
            using (StreamReader reader = new StreamReader(input))
                root = JObject.Parse(reader.ReadToEnd());
        }

        public void Store(Stream output)
        {
            using (StreamWriter writer = new StreamWriter())
                writer.Write(root.ToString(formatting));
        }

        public ICompositeStorage Add(string key, string value)
        {
            root[key] = value;
            return this;
        }

        public ICompositeStorage Add(string key)
        {
            JObject child = new JObject();
            root[key] = child;
            return new JsonCompositeStorage(child, loadSettings, formatting);
        }

        public bool TryGet(string key, out string value)
        {
            JToken valueToken;
            if(root.TryGetValue(key, out valueToken))
            {
                value = valueToken.ToString();
                return true;
            }

            value = null;
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
