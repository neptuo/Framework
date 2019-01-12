using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Neptuo.Windows.HotKeys.Internals
{
    /// <summary>
    /// An internal list of registered handler delegates.
    /// </summary>
    internal class HandlerList : IEnumerable<Action<Key, ModifierKeys>>
    {
        public short Atom { get; }
        public List<Action<Key, ModifierKeys>> Handlers { get; }

        public HandlerList(short atom)
        {
            Atom = atom;
            Handlers = new List<Action<Key, ModifierKeys>>();
        }

        public IEnumerator<Action<Key, ModifierKeys>> GetEnumerator()
            => Handlers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
