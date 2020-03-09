using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class Logger
    {
        /// <summary>
        /// Schreibt eine Log-Zeile ins Log-File
        /// </summary>
        /// <param name="pLogOrigin">Herkunft</param>
        /// <param name="pLogMessage">Meldung</param>
        public static void WriteLogLine(string pLogOrigin, string pLogMessage)
        {
            try
            {
                StreamWriter wrt = new StreamWriter(MyConstants.PATHTOLOGFILE, true);
                wrt.WriteLine(string.Format("{0}: {1}; {2}", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"), pLogOrigin, pLogMessage));
                wrt.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
