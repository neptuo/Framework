using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    public static class Guard
    {
        public static void NotNull(object argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);
        }

        public static void NotNullOrEmpty(string argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);

            if (String.IsNullOrEmpty(argument))
                throw new ArgumentException("Passed argument can't be empty string.", argumentName);
        }

        public static void Positive(int argument, string argumentName)
        {
            if (argument <= 0)
                throw new ArgumentOutOfRangeException(argumentName, "Argument must be positive (> 0).");
        }

        public static void PositiveOrZero(int argument, string argumentName)
        {
            if (argument < 0)
                throw new ArgumentOutOfRangeException(argumentName, "Argument must be positive or zero (>= 0).");
        }

        public static void Negative(int argument, string argumentName)
        {
            if (argument >= 0)
                throw new ArgumentOutOfRangeException(argumentName, "Argument must be negative (< 0).");
        }

        public static void NegativeOrZero(int argument, string argumentName)
        {
            if (argument > 0)
                throw new ArgumentOutOfRangeException(argumentName, "Argument must be negative or zero (<= 0).");
        }
    }
}
