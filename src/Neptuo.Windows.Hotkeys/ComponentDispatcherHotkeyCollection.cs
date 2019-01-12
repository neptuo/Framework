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
    /// An implementation of the <see cref="IHotkeyCollection"/> with <see cref="ComponentDispatcher.ThreadFilterMessage"/>.
    /// </summary>
    public class ComponentDispatcherHotkeyCollection : HotkeyCollectionBase
    {
        /// <summary>
        /// Creates a new instance and binds to <see cref="ComponentDispatcher.ThreadFilterMessage"/>.
        /// </summary>
        public ComponentDispatcherHotkeyCollection()
            : base(IntPtr.Zero)
        {
            ComponentDispatcher.ThreadFilterMessage += OnComponentDispatcherThreadFilterMessage;
        }

        /// <summary>
        /// A method executed when a <see cref="ComponentDispatcher.ThreadFilterMessage"/> raises.
        /// </summary>
        protected void OnComponentDispatcherThreadFilterMessage(ref MSG message, ref bool handled)
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
