using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpKit.UnobtrusiveFeatures
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
            if (appConfig.AppSettings.Settings[key] != null)
                return appConfig.AppSettings.Settings[key].Value;

            return defaultValue;
        }

        /// <summary>
        /// Returns configuration property <paramref name="key"/> or <c>null</c>.
        /// </summary>
        /// <param name="key">The configuration key.</param>
        /// <returns>Configuration property <paramref name="key"/> or <c>null</c>.</returns>
        public string[] GetStringArray(string key)
        {
            string value = GetString(key);
            if (value == null)
                return null;

            return value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
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
