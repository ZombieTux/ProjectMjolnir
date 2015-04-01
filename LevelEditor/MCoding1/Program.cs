using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MCoding1
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Forms.frmDebug());

            using (Test1 test = new Test1())
            {
                test.Run();
            }
        }

    }
}
