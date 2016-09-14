using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpKit.UnobtrusiveFeatures
{
    public class Log
    {
        private string name;
        private bool debug;

        /// <summary>
        /// Inicializuje log (<paramref name="debug"/>) a jméno logu (<paramref name="name"/>), 
        /// to složití při zapisování zpráv do konzole.
        /// </summary>
        /// <param name="name">Jméno logu.</param>
        /// <param name="debug">Zda jsme v debugu.</param>
        public Log(string name, bool debug)
        {
            this.name = name;
            this.debug = debug;
        }

        /// <summary>
        /// Zaloguje zprávu do konzole (pokud je <code>debug == true</code>).
        /// </summary>
        /// <param name="format">Zpráva nebo formátovací řetězec.</param>
        /// <param name="args">Případně argument do formátovacího řetězece.</param>
        public void Debug(string format, params object[] args)
        {
            if (debug)
                Console.WriteLine(name + ": " + format, args);
        }
    }
}
