using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// Helper for throwing exceptions.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if <paramref name="argument"/> is <code>null</code>.
        /// </summary>
        /// <param name="argument">Argument to test.</param>
        /// <param name="argumentName">Argument name.</param>
        public static void NotNull(object argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentException"/> if <paramref name="argument"/> is <code>null</code>.
        /// </summary>
        /// <param name="argument">Argument to test.</param>
        /// <param name="argumentName">Argument name.</param>
        /// <param name="message">Text description.</param>
        public static void NotNull(object argument, string argumentName, string message)
        {
            if (argument == null)
                throw new ArgumentException(message, argumentName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if <paramref name="argument"/> is null.
        /// Throws <see cref="ArgumentException"/> if <paramref name="argument"/> is equal to <code>String.Empty</code>.
        /// </summary>
        /// <param name="argument">Argument to test.</param>
        /// <param name="argumentName">Argument name.</param>
        public static void NotNullOrEmpty(string argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);

            if (String.IsNullOrEmpty(argument))
                throw new ArgumentException("Passed argument can't be empty string.", argumentName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> is <paramref name="argument"/> is lower or equal to zero.
        /// </summary>
        /// <param name="argument">Argument to test.</param>
        /// <param name="argumentName">Argument name.</param>
        public static void Positive(int argument, string argumentName)
        {
            if (argument <= 0)
                throw new ArgumentOutOfRangeException(argumentName, "Argument must be positive (> 0).");
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> is <paramref name="argument"/> is lower than zero.
        /// </summary>
        /// <param name="argument">Argument to test.</param>
        /// <param name="argumentName">Argument name.</param>
        public static void PositiveOrZero(int argument, string argumentName)
        {
            if (argument < 0)
                throw new ArgumentOutOfRangeException(argumentName, "Argument must be positive or zero (>= 0).");
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> is <paramref name="argument"/> is greater or equal to zero.
        /// </summary>
        /// <param name="argument">Argument to test.</param>
        /// <param name="argumentName">Argument name.</param>
        public static void Negative(int argument, string argumentName)
        {
            if (argument >= 0)
                throw new ArgumentOutOfRangeException(argumentName, "Argument must be negative (< 0).");
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> is <paramref name="argument"/> is greater than zero.
        /// </summary>
        /// <param name="argument">Argument to test.</param>
        /// <param name="argumentName">Argument name.</param>
        public static void NegativeOrZero(int argument, string argumentName)
        {
            if (argument > 0)
                throw new ArgumentOutOfRangeException(argumentName, "Argument must be negative or zero (<= 0).");
        }
    }
}
