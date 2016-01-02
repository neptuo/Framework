using Neptuo.Exceptions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// Keys extensions of <see cref="Ensure.Condition"/>.
    /// </summary>
    public static class _EnsureConditionExtensions
    {
        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="key"/> is <see cref="IKey.Empty"/> (= true).
        /// </summary>
        /// <param name="condition">The condition helper.</param>
        /// <param name="key">The to test.</param>
        /// <param name="argumentName">The name of the key argument.</param>
        public static void NotEmpty(this EnsureConditionHelper condition, IKey key, string argumentName)
        {
            Ensure.NotNull(condition, "condition");
            Ensure.NotNull(key, "key");

            if (key.IsEmpty)
                throw Ensure.Exception.ArgumentOutOfRange(argumentName, "Argument requires not empty key.");
        }
    }
}
