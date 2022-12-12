using LunarBase.Utilidades;
using System;
using System.Windows.Forms;

namespace Lunar
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {   
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Sessao.syncfusion);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
            //Application.Run(new FrmPrincipal());
        }
    }
}
