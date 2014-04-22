using Neptuo.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Configuration
{
    class TestConfiguration
    {
        public static void Test()
        {
            IConfigurationScopeRegistry registry = new ConfigurationScopeRegistry();
            registry.MapScope("Static", new StaticConfigurationScope());

            ConfigurationBase configuration = new ConfigurationBase(registry, new EmptyConfigurationScope());
            configuration.MapProperty<IsDebugProperty>();

            ModuleConfiguration module = new ModuleConfiguration(configuration);
            Console.WriteLine(module.IsDebug);
        }
    }

    class IsDebugProperty : BoolConfigurationProperty
    {
        public IsDebugProperty()
            : base(false, "Static")
        { }
    }

    class ModuleConfiguration
    {
        private IConfiguration configuration;

        public bool IsDebug
        {
            get { return configuration.GetProperty<IsDebugProperty>().Value; }
        }

        public ModuleConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
