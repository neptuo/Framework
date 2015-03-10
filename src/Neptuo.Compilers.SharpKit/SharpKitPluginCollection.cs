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
    public class SharpKitPluginCollection : IEnumerable<string>
    {
        private readonly List<string> storage = new List<string>();

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
    }
}
