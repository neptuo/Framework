using System;

namespace SharpKit.JavaScript.Private
{
    /// <summary>
    /// Represents a pseudo-random number generator, a device that produces a 
    /// sequence of numbers that meet certain statistical requirements for randomness.
    /// </summary>
    [JsType(Name = "System.Random", Filename = "~/Internal/Core.js")]
    public class JsImplRandom
    {
        #region Constants
        /// <summary>
        /// The maximum value
        /// </summary>
        public const int MaxValue = 0x7FFFFFFF;
        #endregion

        #region Initialization
        /// <summary>
        /// Creates a random number generator using the time of day in milliseconds as
        /// the seed.
        /// </summary>
        public JsImplRandom()
        {
        }

        /// <summary>
        /// Creates a random number generator initialized with the given seed. 
        /// </summary>
        /// <param name="seed">The seed.</param>
        public JsImplRandom(int seed)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Member Functions
        /// <summary>
        /// Returns a random integer greater than or equal to zero and
        /// less than or equal to <c>MaxRandomInt</c>. 
        /// </summary>
        /// <returns>The next random integer.</returns>
        public int Next()
        {
            return JsMath.floor(JsMath.random()*MaxValue);
        }

        /// <summary>
        /// Returns a positive random integer less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The maximum value. Must be greater than zero.</param>
        /// <returns>A positive random integer less than or equal to <c>maxValue</c>.</returns>
        public int Next(int maxValue)
        {
            return JsMath.floor(JsMath.random() * maxValue);
        }

        /// <summary>
        /// Returns a random integer within the specified range.
        /// </summary>
        /// <param name="minValue">The lower bound.</param>
        /// <param name="maxValue">The upper bound.</param>
        /// <returns>A random integer greater than or equal to <c>minValue</c>, and less than
        /// or equal to <c>maxValue</c>.</returns>
        public int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue) throw new ArgumentOutOfRangeException();
            return (JsMath.floor((maxValue - minValue) * JsMath.random() + minValue)).As<int>();
        }

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// </summary>
        /// <returns>A single-precision floating point number greater than or equal to 0.0, 
        /// and less than 1.0.</returns>
        public float NextFloat()
        {
            return JsMath.random();
        }

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public void NextBytes(byte[] buffer)
        {
            for (int l = 0; l < buffer.LongLength; l++)
                buffer[l] = Next(0x00, 0x100).As<byte>();
        }

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// </summary>
        /// <returns>A double-precision floating point number greater than or equal to 0.0, 
        /// and less than 1.0.</returns>
        public double NextDouble()
        {
            return JsMath.random();
        }
        #endregion
    }
}
