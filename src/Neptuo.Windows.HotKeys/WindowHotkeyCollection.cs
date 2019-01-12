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
    /// An implementation of <see cref="IHotkeyCollection"/> that binds to instance of <see cref="Window"/>.
    /// </summary>
    public class WindowHotKeyCollection : HotkeyCollectionBase
    {
        /// <summary>
        /// Gets a window that a this instance is bound to.
        /// </summary>
        public Window Window { get; protected set; }

        /// <summary>
        /// Gets a HWnd source of <see cref="Window"/> that a this instance is bound to.
        /// </summary>
        public HwndSource HWndSource { get; protected set; }

        /// <summary>
        /// Creates new instance bound to a <paramref name="window"/>.
        /// </summary>
        /// <param name="window">A window this instance will be bound to.</param>
        public WindowHotKeyCollection(Window window)
            : base(CreateInteropHelper(window).Handle)
        {
            Ensure.NotNull(window, "window");
            Window = window;

            HWndSource = HwndSource.FromHwnd(Handle);
            HWndSource.AddHook(WindowProcHook);
        }

        private static WindowInteropHelper CreateInteropHelper(Window window)
        {
            Ensure.NotNull(window, "window");
            WindowInteropHelper helper = new WindowInteropHelper(window);
            return helper;
        }

        /// <summary>
        /// A method executed when a windows gets a message.
        /// </summary>
        /// <returns><see cref="IntPtr.Zero"/>.</returns>
        protected IntPtr WindowProcHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case Win32.WM_HOTKEY:
                    handled = OnHotKey(KeyInterop.KeyFromVirtualKey(((int)lParam >> 16) & 0xFFFF), (ModifierKeys)((int)lParam & 0xFFFF));
                    break;
            }

            return IntPtr.Zero;
        }
    }
}
