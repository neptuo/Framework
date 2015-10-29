using Neptuo.Windows.HotKeys.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interop;

namespace Neptuo.Windows.HotKeys
{
    /// <summary>
    /// Implementation of <see cref="IHotKeyCollection"/> with <see cref="ComponentDispatcher.ThreadFilterMessage"/>.
    /// </summary>
    public class ComponentDispatcherHotKeyCollection : HotkeyCollectionBase
    {
        /// <summary>
        /// Creates new instance and binds to <see cref="ComponentDispatcher.ThreadFilterMessage"/>.
        /// </summary>
        public ComponentDispatcherHotKeyCollection()
            : base(IntPtr.Zero)
        {
            ComponentDispatcher.ThreadFilterMessage += new ThreadMessageEventHandler(OnComponentDispatcherThreadFilterMessage);
        }

        private void OnComponentDispatcherThreadFilterMessage(ref MSG message, ref bool handled)
        {
            if (!handled)
            {
                if (message.message == Win32.WM_HOTKEY)
                {
                    Key key = KeyInterop.KeyFromVirtualKey(((int)message.lParam >> 16) & 0xFFFF);
                    ModifierKeys modifier = (ModifierKeys)((int)message.lParam & 0xFFFF);
                    OnHotKey(key, modifier);
                    handled = true;
                }
            }
        }
    }
}
