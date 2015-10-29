using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Neptuo.Windows.HotKeys
{
    /// <summary>
    /// Collection of registered hot keys.
    /// </summary>
    public interface IHotkeyCollection
    {
        /// <summary>
        /// Adds <paramref name="handler"/> to be triggered when <paramref name="key"/> and <paramref name="modifier"/> is pressed.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="modifier">Key modifiers.</param>
        /// <param name="handler">Handler to be executed.</param>
        /// <returns>Self (for fluency).</returns>
        IHotkeyCollection Add(Key key, ModifierKeys modifier, Action<Key, ModifierKeys> handler);

        /// <summary>
        /// Removes all handlers on <paramref name="key"/> and <paramref name="modifier"/>.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="modifier">Key modifiers.</param>
        /// <returns>Self (for fluency).</returns>
        IHotkeyCollection Remove(Key key, ModifierKeys modifier);

        /// <summary>
        /// Removes <paramref name="handler"/> from all hot key bindings.
        /// </summary>
        /// <param name="handler">Handler to be removed.</param>
        /// <returns>Self (for fluency).</returns>
        IHotkeyCollection Remove(Action<Key, ModifierKeys> handler);
    }
}
