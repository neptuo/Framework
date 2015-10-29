using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Neptuo.Windows.HotKeys.Internals
{
    internal class HandlerList : IEnumerable<Action<Key, ModifierKeys>>
    {
        public short Atom { get; private set; }
        public List<Action<Key, ModifierKeys>> Handlers { get; private set; }

        public HandlerList(short atom)
        {
            Atom = atom;
            Handlers = new List<Action<Key, ModifierKeys>>();
        }

        public IEnumerator<Action<Key, ModifierKeys>> GetEnumerator()
        {
            return Handlers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
