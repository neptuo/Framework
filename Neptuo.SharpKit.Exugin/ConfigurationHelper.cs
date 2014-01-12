using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.SharpKit.Exugin
{
    /// <summary>
    /// Helper pro vytažení hodnot z konfigurace.
    /// </summary>
    public class ConfigurationHelper
    {
        private Configuration appConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Vrací string hodnotu přiřazenou k <paramref name="key"/>.
        /// Případně vrací <paramref name="defaultValue"/> pokud nebyl klíč nalezen.
        /// </summary>
        /// <param name="key">Klíč do konfigurace.</param>
        /// <param name="defaultValue">Výchozí hodnota.</param>
        /// <returns>Hodnotu klíče <paramref name="key"/> nebo <paramref name="defaultValue"/>.</returns>
        public string GetString(string key, string defaultValue = null)
        {
            if (appConfig.AppSettings.Settings["key"] != null)
                return appConfig.AppSettings.Settings["key"].Value;

            return defaultValue;
        }

        /// <summary>
        /// Vrací bool hodnotu přiřazenou k <paramref name="key"/>.
        /// Případně vrací <paramref name="defaultValue"/> pokud nebyl klíč nalezen nebo nelze hodnotu převést na bool.
        /// </summary>
        /// <param name="key">Klíč do konfigurace.</param>
        /// <param name="defaultValue">Výchozí hodnota.</param>
        /// <returns>Hodnotu klíče <paramref name="key"/> nebo <paramref name="defaultValue"/>.</returns>
        public bool GetBool(string key, bool defaultValue = false)
        {
            bool value;
            if (Boolean.TryParse(GetString(key), out value))
                return value;

            return defaultValue;
        }
    }
}
