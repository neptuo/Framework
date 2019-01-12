using Neptuo.Windows.HotKeys.Internals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Neptuo.Windows.HotKeys
{
    /// <summary>
    /// A base implementation of registering and firing hot keys.
    /// </summary>
    public abstract class HotkeyCollectionBase : DisposableBase, IHotkeyCollection
    {
        private readonly Dictionary<KeyModel, HandlerList> storage = new Dictionary<KeyModel, HandlerList>();

        /// <summary>
        /// Gets a handler bound to.
        /// </summary>
        protected IntPtr Handle { get; }

        /// <summary>
        /// Creates a new instance which binds to <paramref name="handle"/>.
        /// </summary>
        /// <param name="handle">A windows handle to bind to.</param>
        public HotkeyCollectionBase(IntPtr handle)
        {
            Ensure.NotNull(handle, "handle");
            Handle = handle;
        }

        /// <summary>
        /// Executed when hotkey is pressed.
        /// </summary>
        /// <param name="key">A pressed key.</param>
        /// <param name="modifiers">A pressed modifiers.</param>
        /// <returns><c>true</c> if handler exists; otherwise <c>false</c>.</returns>
        protected bool OnHotKey(Key key, ModifierKeys modifiers)
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
                if (Win32.RegisterHotKey(Handle, handlers.Atom, (uint)hotkey.Modifier, (uint)KeyInterop.VirtualKeyFromKey(hotkey.Key)))
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
                    Win32.UnregisterHotKey(Handle, handlers.Atom);
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
                Win32.UnregisterHotKey(Handle, handlers.Atom);
                Win32.GlobalDeleteAtom(handlers.Atom);
            }

            storage.Clear();
        }
    }
}
