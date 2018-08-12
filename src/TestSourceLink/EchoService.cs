using System;

namespace TestSourceLink
{
    /// <summary>
    /// An echo service.
    /// </summary>
    public class EchoService
    {
        /// <summary>
        /// Outputs greeting.
        /// </summary>
        /// <param name="name">A caller name.</param>
        public static void SayHello(string name) => Console.WriteLine($"Hello, {name}!");
    }
}
