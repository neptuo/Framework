using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Configuration of the compiler.
    /// </summary>
    public class CompilerConfiguration : KeyValueCollection, ICompilerConfiguration
    {
        /// <summary>
        /// Creates empty configuration.
        /// </summary>
        public CompilerConfiguration()
        { }

        /// <summary>
        /// Creates new instance from <paramref name="parentConfiguration"/>.
        /// </summary>
        /// <param name="parentConfiguration">Source configuration values.</param>
        public CompilerConfiguration(ICompilerConfiguration parentConfiguration)
            : base(parentConfiguration)
        { }

        /// <summary>
        /// Creates deep copy of this instance.
        /// </summary>
        /// <returns>New instance with values copied from this instance.</returns>
        public CompilerConfiguration Copy()
        {
            return new CompilerConfiguration(this);
        }

        public ICompilerConfiguration Clone()
        {
            CompilerConfiguration result = new CompilerConfiguration();

            IEnumerable<string> keys = Keys;
            foreach (string key in keys)
            {
                object value = this.Get<object>(key);
                ICloneable<object> cloneable = value as ICloneable<object>;
                if (cloneable != null)
                    result.Add(key, cloneable.Clone());
                else
                    result.Add(key, value);
            }

            return result;
        }
    }
}
