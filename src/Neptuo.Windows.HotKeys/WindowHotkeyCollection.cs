using Neptuo.Windows.HotKeys.Internals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace Neptuo.Windows.HotKeys
{
    /// <summary>
    /// Implementation of <see cref="IHotkeyCollection"/> that binds to instance of <see cref="Window"/>.
    /// </summary>
    public class WindowHotkeyCollection : DisposableBase, IHotkeyCollection
    {
        private readonly Dictionary<KeyModel, HandlerList> storage = new Dictionary<KeyModel, HandlerList>();

        public Window Window { get; protected set; }
        public HwndSource HWndSource { get; protected set; }
        public WindowInteropHelper WindowInteropHelper { get; protected set; }

        public WindowHotkeyCollection(Window window)
        {
            Ensure.NotNull(window, "window");
            Window = window;
            WindowInteropHelper = new WindowInteropHelper(Window);
            HWndSource = HwndSource.FromHwnd(WindowInteropHelper.Handle);

            HWndSource.AddHook(MainWindowProc);
        }

        public IntPtr MainWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case Win32.WM_HOTKEY:
                    handled = OnHotKey((ModifierKeys)((int)lParam & 0xFFFF), KeyInterop.KeyFromVirtualKey(((int)lParam >> 16) & 0xFFFF));
                    break;
            }
            return IntPtr.Zero;
        }

        private bool OnHotKey(ModifierKeys modifiers, Key key)
        {
            KeyModel hotkey = new KeyModel(modifiers, key);
            HandlerList handlers;
            if (storage.TryGetValue(hotkey, out handlers))
            {
                foreach (Action<Key, ModifierKeys> handler in handlers)
                    handler(key, modifiers);

                return true;
            }

            return false;
        }


        public IHotkeyCollection Add(Key key, ModifierKeys modifier, Action<Key, ModifierKeys> handler)
        {
            KeyModel hotkey = new KeyModel(modifier, key);
            HandlerList handlers;
            if (!storage.TryGetValue(hotkey, out handlers))
            {
                handlers = new HandlerList((short)hotkey.GetHashCode());
                if (Win32.RegisterHotKey(WindowInteropHelper.Handle, handlers.Atom, (uint)hotkey.Modifier, (uint)KeyInterop.VirtualKeyFromKey(hotkey.Key)))
                    storage[hotkey] = handlers;
                else
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            handlers.Handlers.Add(handler);
            return this;
        }

        public IHotkeyCollection Remove(Key key, ModifierKeys modifier)
        {
            KeyModel hotkey = new KeyModel(modifier, key);
            HandlerList handlers;
            if (storage.TryGetValue(hotkey, out handlers))
            {
                if (storage.Remove(hotkey))
                    Win32.UnregisterHotKey(WindowInteropHelper.Handle, handlers.Atom);
            }

            return this;
        }

        public IHotkeyCollection Remove(Action<Key, ModifierKeys> handler)
        {
            foreach (KeyValuePair<KeyModel, HandlerList> handlers in storage)
            {
                if (handlers.Value.Handlers.Contains(handler))
                {
                    handlers.Value.Handlers.Remove(handler);
                    if (handlers.Value.Handlers.Count == 0)
                        return Remove(handlers.Key.Key, handlers.Key.Modifier);
                }
            }

            return this;
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();

            foreach (HandlerList handlers in storage.Values)
            {
                Win32.UnregisterHotKey(WindowInteropHelper.Handle, handlers.Atom);
                Win32.GlobalDeleteAtom(handlers.Atom);
            }

            storage.Clear();
        }
    }
}
