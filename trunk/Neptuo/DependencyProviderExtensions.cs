﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo
{
    public static class DependencyProviderExtensions
    {
        public static T Resolve<T>(this IDependencyProvider provider)
        {
            return (T)provider.Resolve(typeof(T), null);
        }

        public static object Resolve(this IDependencyProvider provider, Type type)
        {
            return provider.Resolve(type, null);
        }
    }
        
    public static class DependencyContainerExtensions
    {
        public static IDependencyContainer RegisterInstance<T>(this IDependencyContainer container, T instance)
        {
            return container.RegisterInstance(typeof(T), null, instance);
        }

        public static IDependencyContainer RegisterType<TFrom, TTo>(this IDependencyContainer container)
        {
            return container.RegisterType(typeof(TFrom), typeof(TTo), null, null);
        }

        public static IDependencyContainer RegisterType(this IDependencyContainer container, Type from, Type to, string name)
        {
            return container.RegisterType(from, to, name, null);
        }

        public static IDependencyContainer RegisterType(this IDependencyContainer container, Type from, object lifetime)
        {
            return container.RegisterType(from, from, null, lifetime);
        }

        public static IDependencyContainer RegisterType<TFrom>(this IDependencyContainer container, object lifetime)
        {
            return container.RegisterType(typeof(TFrom), typeof(TFrom), null, lifetime);
        }

        public static IDependencyContainer RegisterType<TFrom, TTo>(this IDependencyContainer container, object lifetime)
        {
            return container.RegisterType(typeof(TFrom), typeof(TTo), null, lifetime);
        }

    }
}
