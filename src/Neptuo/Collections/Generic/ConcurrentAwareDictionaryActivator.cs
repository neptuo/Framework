using Neptuo.Activators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Generic
{
    /// <summary>
    /// Factory for dictionaries that creates concurrent of not concurrent instances based on configuration,
    /// either ctor parameter or <c>Environment.WithIsConcurrentApplication</c>.
    /// </summary>
    /// <typeparam name="TKey">Type of key.</typeparam>
    /// <typeparam name="TValue">Type of value.</typeparam>
    public class ConcurrentAwareDictionaryActivator<TKey, TValue> : IActivator<IDictionary<TKey, TValue>>
    {
        private bool isConcurrent;

        public ConcurrentAwareDictionaryActivator()
            : this(Engine.Environment.WithIsConcurrentApplication())
        { }

        public ConcurrentAwareDictionaryActivator(bool isConcurrent)
        {
            this.isConcurrent = isConcurrent;
        }

        public IDictionary<TKey, TValue> Create()
        {
            if (isConcurrent)
                return new Dictionary<TKey, TValue>();
            else
                return new ConcurrentDictionary<TKey, TValue>();
        }

        private static ConcurrentAwareDictionaryActivator<TKey, TValue> instance;
        private static object instanceLock = new object();

        /// <summary>
        /// Default instance.
        /// </summary>
        public static ConcurrentAwareDictionaryActivator<TKey, TValue> Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                            instance = new ConcurrentAwareDictionaryActivator<TKey, TValue>();
                    }
                }

                return instance;
            }
        }
    }
}
