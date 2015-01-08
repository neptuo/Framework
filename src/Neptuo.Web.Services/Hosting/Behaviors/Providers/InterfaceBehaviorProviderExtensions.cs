using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors.Providers
{
    /// <summary>
    /// <see cref="InterfaceBehaviorProvider"/> extensions for mapping http methods.
    /// </summary>
    public static class InterfaceBehaviorProviderExtensions
    {
        /// <summary>
        /// Maps <typeparamref name="TBehavior"/> to <typeparamref name="TImplementation"/>.
        /// </summary>
        /// <typeparam name="TBehavior">Behavior definition.</typeparam>
        /// <typeparam name="TImplementation">Behavior implementation.</typeparam>
        /// <param name="provider">Interface behavior provider.</param>
        /// <returns><paramref name="provider"/> (for fluency).</returns>
        public static InterfaceBehaviorProvider AddMapping<TBehavior, TImplementation>(this InterfaceBehaviorProvider provider)
            where TImplementation : IBehavior<TBehavior>
        {
            Guard.NotNull(provider, "provider");
            return provider
                .AddMapping(typeof(TBehavior), typeof(TImplementation));
        }
    }
}
