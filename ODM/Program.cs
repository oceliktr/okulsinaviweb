using System;
using System.Windows.Forms;

namespace ODM
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girişi.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormGiris());
        }
    }
}
