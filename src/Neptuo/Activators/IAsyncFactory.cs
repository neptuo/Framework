using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// An asynchronous activator for <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">A type of the service to create.</typeparam>
    public interface IAsyncFactory<T>
    {
        /// <summary>
        /// Creates service of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>An instance of the type <typeparamref name="T"/>.</returns>
        Task<T> Create();
    }

    /// <summary>
    /// An asynchronous activator for <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">A type of the service to create.</typeparam>
    /// <typeparam name="TContext">A type of the activation parameter.</typeparam>
    public interface IAsyncFactory<T, in TContext>
    {
        /// <summary>
        /// Creates a service of the type <typeparamref name="T"/> with <paramref name="context"/> as activation parameter.
        /// </summary>
        /// <param name="context">An activation context.</param>
        /// <returns>An instance of the type <typeparamref name="T"/>.</returns>
        Task<T> Create(TContext context);
    }
}
