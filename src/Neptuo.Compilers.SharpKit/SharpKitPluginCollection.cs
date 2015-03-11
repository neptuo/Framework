using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Collection of SharpKit plugins.
    /// </summary>
    public class SharpKitPluginCollection : IEnumerable<string>, ICloneable<SharpKitPluginCollection>
    {
        private readonly List<string> storage;

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public SharpKitPluginCollection()
        {
            storage = new List<string>();
        }

        /// <summary>
        /// Creates new instance with initial enumeration of plugins.
        /// </summary>
        /// <param name="plugins">Initial enumeration of plugins.</param>
        public SharpKitPluginCollection(IEnumerable<string> plugins)
            : this()
        {
            storage.AddRange(plugins);
        }

        /// <summary>
        /// Adds SharpKit plugin to the configuration.
        /// </summary>
        /// <param name="typeAssemblyName">Assembly qualified type name.</param>
        /// <returns>Self (for fluency).</returns>
        public SharpKitPluginCollection Add(string typeAssemblyName)
        {
            Ensure.NotNullOrEmpty(typeAssemblyName, "typeAssemblyName");
            storage.Add(typeAssemblyName);
            return this;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return storage.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return storage.GetEnumerator();
        }

        public SharpKitPluginCollection Clone()
        {
            return new SharpKitPluginCollection(storage);
        }
    }
}
