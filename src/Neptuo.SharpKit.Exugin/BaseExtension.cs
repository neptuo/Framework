using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpKit.UnobtrusiveFeatures
{
    /// <summary>
    /// Bázové rozšíření pluginu. Umožňuje logovat informace na základně nastavení <see cref="debug"/>.
    /// </summary>
    public abstract class BaseExtension
    {
        protected Log Log { get; private set; }

        /// <summary>
        /// Inicializuje debugování (<paramref name="debug"/>) a jméno rozšíření (<paramref name="extensionName"/>), 
        /// to složití při zapisování zpráv do konzole.
        /// </summary>
        /// <param name="extensionName">Jméno rozšíření.</param>
        /// <param name="debug">Zda jsme v debugu.</param>
        public BaseExtension(string extensionName, bool debug)
        {
            Log = new Log(extensionName, debug);
        }

        /// <summary>
        /// Zaloguje zprávu do konzole (pokud je <code>debug == true</code>).
        /// </summary>
        /// <param name="format">Zpráva nebo formátovací řetězec.</param>
        /// <param name="args">Případně argument do formátovacího řetězece.</param>
        protected void LogDebug(string format, params object[] args)
        {
            Log.Debug(format, args);
        }
    }
}
